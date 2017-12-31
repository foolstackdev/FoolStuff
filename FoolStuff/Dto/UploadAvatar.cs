using FoolStuff.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoolStuff.Dto
{
    public class UploadAvatar
    {
        public List<AvatarImages> avatarImages { get; set; }

    }
    public class AvatarImages
    {
        public string size { get; set; }
        public string data { get; set; }
        public string name { get; set; }
        public string type { get; set; }

        public AvatarImages getUserAvatar(string id)
        {

            return this;
        }
    }
}