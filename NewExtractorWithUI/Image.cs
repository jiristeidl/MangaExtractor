using System.IO.Compression;

namespace NewExtractorWithUI
{
    internal class Image : CustomFile
    {
        new public ZipArchiveEntry Name { get; set; }
    }
}