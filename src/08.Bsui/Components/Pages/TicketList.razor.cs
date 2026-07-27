using MudBlazor;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.DTOs.Users;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Bsui.Components.Pages
{
    public partial class TicketList
    {
        private MudTable<TicketDto>? _table;

        private bool _isLoading;

        private string? _searchText;
        private TicketStatus? _selectedStatus;
        private Guid? _selectedAgentId;
        private TicketPriority? _selectedPriority;
        private TicketCategory? _selectedCategory;

        private List<UserDto> _agentList = new();
        private List<TicketDto> _currentPageTickets = new();

        private readonly HashSet<Guid> _selectedTicketIds = new();

        private int TotalTicketCount { get; set; }
        private int OpenTicketCount { get; set; }
        private int InProgressTicketCount { get; set; }
        private int ClosedTicketCount { get; set; }
        //private int CancelledTicketCount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.WhenAll(
                LoadAgentsAsync(),
                LoadSummaryAsync());
        }

        private async Task LoadAgentsAsync()
        {
            try
            {
                var response = await UserClient.GetAllAgentsAsync();

                if (response?.Success == true && response.Data is not null)
                {
                    _agentList = response.Data.ToList();
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(
                    $"Gagal mengambil daftar agent: {ex.Message}",
                    Severity.Warning);
            }
        }

        private async Task LoadSummaryAsync()
        {
            try
            {
                var totalTask = GetTicketCountAsync(null);
                var openTask = GetTicketCountAsync(TicketStatus.Open);
                var inProgressTask = GetTicketCountAsync(TicketStatus.InProgress);
                var closedTask = GetTicketCountAsync(TicketStatus.Closed);

                await Task.WhenAll(
                    totalTask,
                    openTask,
                    inProgressTask,
                    closedTask);

                TotalTicketCount = totalTask.Result;
                OpenTicketCount = openTask.Result;
                InProgressTicketCount = inProgressTask.Result;
                ClosedTicketCount = closedTask.Result;
            }
            catch (Exception ex)
            {
                Snackbar.Add(
                    $"Gagal mengambil summary tiket: {ex.Message}",
                    Severity.Warning);
            }
        }

        private async Task<int> GetTicketCountAsync(TicketStatus? status)
        {
            var request = new PagedRequest
            {
                PageNumber = 1,
                PageSize = 1
            };

            var response = await TicketClient.GetFilteredTicketsAsync(
                status?.ToString(),
                null,
                request);

            return response.TotalCount;
        }

        private async Task<TableData<TicketDto>> ServerReloadAsync(TableState state, CancellationToken cancellationToken)
        {
            _isLoading = true;

            try
            {
                var request = new PagedRequest
                {
                    PageNumber = state.Page + 1,
                    PageSize = state.PageSize
                };

                var response = await TicketClient.GetTicketListAsync(
                    _selectedStatus?.ToString(),
                    _selectedAgentId,
                    request,
                    _selectedPriority?.ToString(),
                    _selectedCategory?.ToString(),
                    _searchText);

                var tickets = response.Items?.ToList() ?? new List<TicketDto>();

                _currentPageTickets = tickets;

                return new TableData<TicketDto>
                {
                    Items = tickets,
                    TotalItems = response.TotalCount
                };
            }
            catch (Exception ex)
            {
                Snackbar.Add(
                    $"Gagal mengambil tiket: {ex.Message}",
                    Severity.Error);

                _currentPageTickets.Clear();

                return new TableData<TicketDto>
                {
                    Items = Array.Empty<TicketDto>(),
                    TotalItems = 0
                };
            }
            finally
            {
                _isLoading = false;
            }
        }
        private bool AreAllCurrentPageTicketsSelected =>
            _currentPageTickets.Count > 0 &&
            _currentPageTickets.All(ticket =>
                _selectedTicketIds.Contains(ticket.Id));

        private bool IsTicketSelected(Guid ticketId)
        {
            return _selectedTicketIds.Contains(ticketId);
        }

        private void OnTicketSelectionChanged(
            Guid ticketId,
            bool isSelected)
        {
            if (isSelected)
            {
                _selectedTicketIds.Add(ticketId);
                return;
            }

            _selectedTicketIds.Remove(ticketId);
        }

        private void OnSelectAllChanged(bool isSelected)
        {
            foreach (var ticket in _currentPageTickets)
            {
                OnTicketSelectionChanged(ticket.Id, isSelected);
            }
        }

        private async Task OnSearchChanged(string? value)
        {
            _searchText = value;
            await ReloadFirstPageAsync();
        }


        private async Task OnStatusChanged(TicketStatus? value)
        {
            _selectedStatus = value;
            await ReloadFirstPageAsync();
        }

        private async Task OnAssigneeChanged(Guid? value)
        {
            _selectedAgentId = value;
            await ReloadFirstPageAsync();
        }

        private async Task OnPriorityChanged(TicketPriority? value)
        {
            _selectedPriority = value;
            await ReloadFirstPageAsync();
        }

        private async Task OnCategoryChanged(TicketCategory? value)
        {
            _selectedCategory = value;
            await ReloadFirstPageAsync();
        }

        private async Task ApplyFilter()
        {
            await ReloadFirstPageAsync();
        }

        private async Task ResetFilter()
        {
            _searchText = null;
            _selectedStatus = null;
            _selectedAgentId = null;
            _selectedPriority = null;
            _selectedCategory = null;

            await ReloadFirstPageAsync();
        }

        private async Task ReloadFirstPageAsync()
        {
            if (_table is null)
            {
                return;
            }

            _table.NavigateTo(0);
            await _table.ReloadServerData();
        }

        private string GetPercentage(int count)
        {
            if (TotalTicketCount == 0)
            {
                return "0.0%";
            }

            return $"{count * 100d / TotalTicketCount:0.0}%";
        }

        private string GetAgentName(Guid? agentId)
        {
            if (!agentId.HasValue)
            {
                return "Unassigned";
            }

            var agent = _agentList.FirstOrDefault(
                item => item.Id == agentId.Value);

            return agent?.Name ?? "Unknown Agent";
        }

        private static string GetStatusText(TicketStatus status)
        {
            return status switch
            {
                TicketStatus.InProgress => "In Progress",
                TicketStatus.Resolved => "Resolved",
                TicketStatus.Closed => "Closed",
                _ => "Open"
            };
        }

        private static string GetStatusStyle(TicketStatus status)
        {
            var backgroundColor = "#EEF2F7";
            var textColor = "#64748B";

            switch (status)
            {
                case TicketStatus.Open:
                    backgroundColor = "#E8F2FF";
                    textColor = "#1683FF";
                    break;

                case TicketStatus.InProgress:
                    backgroundColor = "#FFF4DB";
                    textColor = "#F59E0B";
                    break;

                case TicketStatus.Closed:
                    backgroundColor = "#E8F8EE";
                    textColor = "#22A95A";
                    break;

                case TicketStatus.Resolved:
                    backgroundColor = "#E8F8EE";
                    textColor = "#22A95A";
                    break;
            }

            return "height:22px;" +
                   "min-height:22px;" +
                   "padding:0 8px;" +
                   "border:none;" +
                   "border-radius:5px;" +
                   "box-shadow:none;" +
                   $"background-color:{backgroundColor} !important;" +
                   $"color:{textColor} !important;" +
                   "font-size:11px;" +
                   "font-weight:600;" +
                   "line-height:22px;";
        }

        private static string GetPriorityStyle(TicketPriority priority)
        {
            var backgroundColor = "#EEF2F7";
            var textColor = "#64748B";

            switch (priority)
            {
                case TicketPriority.High:
                    backgroundColor = "#FDEBEC";
                    textColor = "#EF4444";
                    break;

                case TicketPriority.Medium:
                    backgroundColor = "#FFF4DB";
                    textColor = "#F59E0B";
                    break;

                case TicketPriority.Low:
                    backgroundColor = "#E8F8EE";
                    textColor = "#22A95A";
                    break;
            }

            return "height:22px;" +
                   "min-height:22px;" +
                   "padding:0 8px;" +
                   "border:none;" +
                   "border-radius:5px;" +
                   "box-shadow:none;" +
                   $"background-color:{backgroundColor} !important;" +
                   $"color:{textColor} !important;" +
                   "font-size:11px;" +
                   "font-weight:600;" +
                   "line-height:22px;";
        }

        private static string GetAssigneeAvatarStyle(string assignee)
        {
            var backgroundColor = "#E9EEF5";
            var textColor = "#64748B";

            switch (assignee)
            {
                case "Andi Pratama":
                    backgroundColor = "#E7E5FF";
                    textColor = "#5B5FE9";
                    break;

                case "Siti Aisyah":
                    backgroundColor = "#FFE5EA";
                    textColor = "#FF4D67";
                    break;

                case "Budi Santoso":
                    backgroundColor = "#E4F0FF";
                    textColor = "#2F80ED";
                    break;

                case "Rizky Hidayat":
                    backgroundColor = "#DFF5F3";
                    textColor = "#299C98";
                    break;

                case "Unassigned":
                    backgroundColor = "#E9EEF5";
                    textColor = "#64748B";
                    break;
            }

            return "width:24px;" +
                   "height:24px;" +
                   "min-width:24px;" +
                   "font-size:9px;" +
                   "font-weight:600;" +
                   "border-radius:50%;" +
                   "box-shadow:none;" +
                   $"background-color:{backgroundColor} !important;" +
                   $"color:{textColor} !important;";
        }

        private static string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "-";
            }

            if (string.Equals(
                name,
                "Unassigned",
                StringComparison.OrdinalIgnoreCase))
            {
                return "US";
            }

            var words = name.Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries);

            return string.Concat(
                words.Take(2)
                     .Select(word => char.ToUpperInvariant(word[0])));
        }

        private void CreateTicket()
        {
            // TODO: Ganti dengan NavigationManager.NavigateTo("/tickets/create")
            // setelah halaman create ticket tersedia.
        }

        private void GoToProfile()
        {
            // TODO: NavigationManager.NavigateTo("/profile");
        }

        private void GoToSettings()
        {
            // TODO: NavigationManager.NavigateTo("/settings");
        }

        private void Logout()
        {
            // TODO: Jalankan proses logout.
        }

        private void ViewTicket(TicketDto ticket)
        {
            // TODO: Navigasi ke detail berdasarkan ticket.Id.
        }

        private void EditTicket(TicketDto ticket)
        {
            // TODO: Buka halaman/dialog edit.
        }

        private void AssignTicket(TicketDto ticket)
        {
            // TODO: Buka dialog assign agent.
        }
    }
}
