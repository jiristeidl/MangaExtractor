using System.IO.Compression;

namespace NewExtractorWithUI
{
    internal class Image : File
    {
        new public ZipArchiveEntry Name { get; set; }
    }
}