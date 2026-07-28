using MudBlazor;
using SupportTicketSystem.Bsui.Components.Dialogs;
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
        private int ResolvedTicketCount { get; set; }
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
                    _agentList = response.Data
                        .OrderBy(agent => agent.Id)
                        .ToList();
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
                var resolvedTask = GetTicketCountAsync(TicketStatus.Resolved);

                await Task.WhenAll(
                    totalTask,
                    openTask,
                    inProgressTask,
                    closedTask,
                    resolvedTask);

                TotalTicketCount = totalTask.Result;
                OpenTicketCount = openTask.Result;
                InProgressTicketCount = inProgressTask.Result;
                ClosedTicketCount = closedTask.Result;
                ResolvedTicketCount = resolvedTask.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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

            var response = await TicketClient.GetTicketListAsync(
                status?.ToString(),
                null,
                request,
                null,
                null,
                null);

            return response.TotalCount;
        }

        private async Task<TableData<TicketDto>> ServerReloadAsync(TableState state, CancellationToken cancellationToken)
        {
            _isLoading = true;

            try
            {
                var pageNumber = Math.Max(1, state.Page + 1);
                var pageSize = state.PageSize > 0
                    ? state.PageSize
                    : 10;

                var request = new PagedRequest
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
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
                    MudBlazor.Severity.Error);

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
        //private bool AreAllCurrentPageTicketsSelected =>
        //    _currentPageTickets.Count > 0 &&
        //    _currentPageTickets.All(ticket =>
        //        _selectedTicketIds.Contains(ticket.Id));

        //private bool IsTicketSelected(Guid ticketId)
        //{
        //    return _selectedTicketIds.Contains(ticketId);
        //}

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

        //private void OnSelectAllChanged(bool isSelected)
        //{
        //    foreach (var ticket in _currentPageTickets)
        //    {
        //        OnTicketSelectionChanged(ticket.Id, isSelected);
        //    }
        //}

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

        //private async Task ApplyFilter()
        //{
        //    await ReloadFirstPageAsync();
        //}

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

            if (_table.CurrentPage != 0)
            {
                _table.NavigateTo(Page.First);
                return;
            }

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

        private static string GetAssigneeAvatarStyle(Guid? assigneeId)
        {
            if (!assigneeId.HasValue)
            {
                return BuildAvatarStyle(
                    backgroundColor: "#E9EEF5",
                    textColor: "#64748B");
            }

            var hue = GetStableHue(assigneeId.Value);

            var backgroundColor = $"hsl({hue}, 75%, 92%)";
            var textColor = $"hsl({hue}, 65%, 38%)";

            return BuildAvatarStyle(
                backgroundColor,
                textColor);
        }

        private static int GetStableHue(Guid assigneeId)
        {
            const uint offsetBasis = 2166136261;
            const uint prime = 16777619;

            var hash = offsetBasis;

            foreach (var value in assigneeId.ToByteArray())
            {
                hash ^= value;
                hash *= prime;
            }

            return (int)(hash % 360);
        }

        private static string BuildAvatarStyle(
            string backgroundColor,
            string textColor)
        {
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

        private async Task OpenDetailsDialogAsync(TicketDto ticket)
        {
            var parameters = new DialogParameters
            {
                ["TicketId"] = ticket.Id
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };

            await DialogService.ShowAsync<TicketDetailDialog>(
                $"Detail Tiket - {ticket.TicketNumber}",
                parameters,
                options);
        }

        private async Task OpenAssignDialogAsync(TicketDto ticket, string title = "Assign Agent")
        {
            var parameters = new DialogParameters { ["TicketId"] = ticket.Id, ["Agents"] = _agentList };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialog = await DialogService.ShowAsync<AssignAgentDialog>(title, parameters, options);

            var result = await dialog.Result;

            if (result is null || result.Canceled)
            {
                return;
            }

            if (result.Data is not Guid agentId)
            {
                Snackbar.Add("Please select an agent before saving.", MudBlazor.Severity.Warning);
                return;
            }

            var assignResponse = await TicketClient.AssignTicketAsync(ticket.Id, agentId);

            if (assignResponse != null && assignResponse.Success)
            {
                Snackbar.Add("Agent assigned successfully.", MudBlazor.Severity.Success);
                if (_table is not null)
                {
                    await _table.ReloadServerData();
                }
            }
            else
            {
                Snackbar.Add($"Failed to assign agent: {assignResponse?.Message}", MudBlazor.Severity.Error);
            }
        }
    }
}
