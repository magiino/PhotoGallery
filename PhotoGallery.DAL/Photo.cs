using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DAL
{
    public class Photo
    {
        public Guid IdKey { get; set; } = Guid.NewGuid();


        public string PhotoName { get; set; }

        public DateTime CreatedTime { get; set; }

        public Format Format { get; set; }

        public Resolution Resolution { get; set; }
    }
}
