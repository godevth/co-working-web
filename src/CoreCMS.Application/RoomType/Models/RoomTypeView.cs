using System;
using System.Collections.Generic;
using System.Text;
using CoreCMS.Application.Room.Models;

namespace CoreCMS.Application.RoomType.Models
{
    public class RoomTypeView
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameEN { get; set; }

        public bool InActiveStatus { get; set; }

        public Pictures Picture { get; set; }

    }
}
