using MudBlazor;

namespace SupportTicketSystem.Bsui.Themes
{
    // ============================================================
    // AppThemes.cs
    // Konfigurasi tema tunggal untuk aplikasi
    // ============================================================
    public static class AppThemes
    {
        public static MudTheme DefaultTheme { get; } = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                // Warna Primer disesuaikan agar bersih dan modern
                Primary = "#059669", // Emerald Green (bisa disesuaikan jika gambar menggunakan biru/warna lain)
                Background = "#F9FAFB", // Abu-abu sangat terang untuk latar belakang
                Surface = "#FFFFFF", // Putih bersih untuk Card/Kertas
                AppbarBackground = "#FFFFFF", // Appbar putih
                AppbarText = "#111827", // Teks gelap untuk Appbar putih
                DrawerBackground = "#FFFFFF",
                DrawerText = "#111827",
                TextPrimary = "#111827",
                TextSecondary = "#4B5563",
                LinesDefault = "#E5E7EB",
                ActionDefault = "#4B5563"
            },
            PaletteDark = new PaletteDark()
            {
                // Tetap mempertahankan estetika dark mode yang nyaman
                Primary = "#10B981",
                Background = "#0F172A",
                Surface = "#1E293B",
                AppbarBackground = "#1E293B",
                DrawerBackground = "#1E293B",
                TextPrimary = "#F8FAFC",
                TextSecondary = "#94A3B8",
                LinesDefault = "#334155",
                ActionDefault = "#CBD5E1"
            },
            Typography = new Typography()
            {
                Default = { FontFamily = ["Inter", "Roboto", "Helvetica", "Arial", "sans-serif"] },
                H5 = { FontWeight = "700" },
                H6 = { FontWeight = "700" },
                Button = { FontWeight = "600", TextTransform = "none" } // TextTransform none agar tidak uppercase semua
            },
            LayoutProperties = new LayoutProperties()
            {
                DefaultBorderRadius = "8px" // Sudut sedikit membulat untuk kesan modern
            }
        };
    }
}