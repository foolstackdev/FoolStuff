using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoolStuff.Dto
{
    public class AvatarImagesUsers
    {
        public string userId { get; set; }
        public List<AvatarImages> avatars { get; set; }
    }
}