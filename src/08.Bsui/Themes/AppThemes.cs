using MudBlazor;

namespace SupportTicketSystem.Bsui.Themes
{
    // ============================================================
    // AppThemes.cs
    // Contains all static theme configurations for the application.
    // ============================================================
    public static class AppThemes
    {
        // ========================================================
        // THEME 1: OBSIDIAN (Retro Dark & Purple Accent)
        // ========================================================
        public static MudTheme ObsidianTheme { get; } = new MudTheme()
        {
            PaletteDark = new PaletteDark()
            {
                Background = "#0E0E10",
                Surface = "#17171A",
                DrawerBackground = "#17171A",
                AppbarBackground = "#17171A",
                TextPrimary = "#EBEBEB",
                TextSecondary = "#909098",
                Primary = "#6366F1",
                PrimaryContrastText = "#FFFFFF",
                Success = "#5CC87A",
                Error = "#E05252",
                Warning = "#D4A44C",
                LinesDefault = "#2C2C32",
                LinesInputs = "#3A3A42",
                HoverOpacity = 0.08,
                ActionDisabledBackground = "#202024"
            },
            PaletteLight = new PaletteLight()
            {
                Primary = "#6366F1",
                AppbarBackground = "#6366F1"
            },
            Typography = new Typography(),
            LayoutProperties = new LayoutProperties() { DefaultBorderRadius = "4px" }
        };

        // ========================================================
        // THEME 2: COBALT (True Black & Monospace)
        // ========================================================
        public static MudTheme CobaltTheme { get; } = new MudTheme()
        {
            PaletteDark = new PaletteDark()
            {
                Background = "#000000",
                Surface = "#0A0A0A",
                DrawerBackground = "#000000",
                AppbarBackground = "#000000",
                TextPrimary = "#F4F4F5",
                TextSecondary = "#A1A1AA",
                Primary = "#FFFFFF",
                PrimaryContrastText = "#000000",
                Success = "#10B981",
                Error = "#EF4444",
                Warning = "#F59E0B",
                LinesDefault = "#27272A",
                LinesInputs = "#27272A",
                HoverOpacity = 0.1
            },
            PaletteLight = new PaletteLight()
            {
                Primary = "#000000",
                AppbarBackground = "#000000"
            },
            Typography = new Typography()
            {
                Default = {
                    FontFamily = ["JetBrains Mono", "monospace"]
                },
                H5 = {
                    FontFamily = ["JetBrains Mono", "monospace"],
                    //TextTransform = "lowercase",
                    FontWeight = "700"
                },
                H6 = {
                    FontFamily = ["JetBrains Mono", "monospace"],
                    //TextTransform = "lowercase",
                    FontWeight = "700"
                },
                Button = {
                    FontFamily = ["JetBrains Mono", "monospace"],
                    //TextTransform = "lowercase",
                    FontWeight = "500"
                }
            },
            LayoutProperties = new LayoutProperties() { DefaultBorderRadius = "12px" }
        };

        // ========================================================
        // THEME 3: OBSCURA TERMINAL (True Black & Green Accent)
        // Target visual: terminal/hacker aesthetic
        // Palette sumber: obscura_categories_terminal_mockup.html
        // ========================================================
        public static MudTheme ObscuraTerminalTheme { get; } = new MudTheme()
        {
            PaletteDark = new PaletteDark()
            {
                // --- Backgrounds ---
                Background = "#000000", // .wrap background
                Surface = "#080808", // thead, statusbar, dialog box
                DrawerBackground = "#080808", // sidebar drawer
                AppbarBackground = "#0D0D0D", // appbar / btn-new background

                // --- Text ---
                TextPrimary = "#F4F4F5", // .td-name, .page-title
                TextSecondary = "#71717A", // .p-path, .sb-total
                TextDisabled = "#3F3F46", // .prompt-line, .td-desc, .rec-count

                // --- Accent ---
                // Primary = green terminal (#10B981): dipakai cursor, badge income,
                //           .p-user, MudButton primary, MudCheckbox, dll.
                Primary = "#10B981",
                PrimaryContrastText = "#000000",

                // Secondary = indigo (#6366F1): dipakai .p-host, chip secondary, dll.
                Secondary = "#6366F1",
                SecondaryContrastText = "#FFFFFF",

                // --- Semantic ---
                Success = "#10B981", // sama dengan Primary (income = hijau)
                Error = "#EF4444", // badge expense, delete dialog border
                Warning = "#F59E0B", // warning state umum
                Info = "#6366F1", // info state (pakai warna host/indigo)

                // --- Lines & Borders ---
                // LinesDefault: border tabel, thead
                LinesDefault = "#1C1C1C",
                // LinesInputs: border input field, btn-new
                LinesInputs = "#27272A",
                // TableLines: border antar cell di MudTable
                TableLines = "#1C1C1C",

                // --- Interaction ---
                HoverOpacity = 0.06,  // tbody tr:hover sangat subtle
                ActionDefault = "#52525B", // [edit] button color
                ActionDisabled = "#3F3F46",
                ActionDisabledBackground = "#0D0D0D",

                // --- Overlay (delete dialog) ---
                OverlayDark = "rgba(0,0,0,0.65)",
                OverlayLight = "rgba(0,0,0,0.35)",

                // --- Divider ---
                Divider = "#1C1C1C",
                DividerLight = "#141414",
            },
            PaletteLight = new PaletteLight()
            {
                // Light mode tidak dipakai untuk tema ini,
                // tapi tetap diisi agar tidak fallback ke default MudBlazor
                Primary = "#059669", // emerald-600 untuk light mode
                AppbarBackground = "#111111",
            },
            Typography = new Typography()
            {
                Default = { FontFamily = ["JetBrains Mono", "Fira Code", "monospace"] },
                H5 = {
                    FontFamily = ["JetBrains Mono", "monospace"],
                    FontWeight = "700"
                },
                H6 = {
                    FontFamily = ["JetBrains Mono", "monospace"],
                    FontWeight = "700"
                },
                Button = {
                    FontFamily = ["JetBrains Mono", "monospace"],
                    FontWeight = "500",
                    // TextTransform diset ke "lowercase" di scoped CSS per komponen
                    // agar tidak override global MudBlazor
                },
                Caption = {
                    FontFamily = ["JetBrains Mono", "monospace"],
                    FontSize   = "0.7rem",
                    LetterSpacing = "0.1em"
                }
            },
            LayoutProperties = new LayoutProperties()
            {
                // 2px = sharp corners ala terminal, bukan rounded Material
                DefaultBorderRadius = "2px"
            }
        };
    }
}