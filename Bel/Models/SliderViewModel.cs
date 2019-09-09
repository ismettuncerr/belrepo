using Bel.DataLayer;
using Bel.DataLayer.Model;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bel.Models
{
    public class SliderViewModel : BaseClass
    {
        public List<Slider> Sliders { get; set; }

        public SliderViewModel()
        {
            Sliders = new List<Slider>();
            Sliders = dataClient.SliderRepository.GetAll().ToList();
        }
    }
}