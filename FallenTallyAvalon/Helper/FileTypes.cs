using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTallyAvalon.Helper
{
    public class FileTypes
    {
        public static FilePickerFileType EXCEL { get; } = new("Excel")
        {
            Patterns = new[] { "*.xlsx" },
            AppleUniformTypeIdentifiers = new[] { "public.xlsx" },
            MimeTypes = new[] { "excel/*" }
        };

        public static FilePickerFileType CSV { get; } = new("Csv")
        {
            Patterns = new[] { "*.csv" },
            AppleUniformTypeIdentifiers = new[] { "public.csv" },
            MimeTypes = new[] { "csv/*" }
        };

        public static FilePickerFileType FTSTAMPS { get; } = new("Ftstamps")
        {
            Patterns = new[] { "*.ftstamps" },
            AppleUniformTypeIdentifiers = new[] { "public.ftstamps" },
            MimeTypes = new[] { "ftstamps/*" }
        };
    }
}
