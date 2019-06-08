using Bel.DataLayer;
using Bel.DataLayer.Model;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bel.Models
{
    public class GalleryViewModel : BaseClass
    {
        GalleryRepository galleryRepository = new GalleryRepository();
        public List<Gallery> Galleries { get; set; }

        public GalleryViewModel()
        {
            Galleries = new List<Gallery>();
            Galleries = galleryRepository.GetAll().ToList();
        }
    }
}