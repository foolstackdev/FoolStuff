using FoolStuff.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using FoolStuff.Dto;

namespace FoolStuff.Manager
{
    public class Avatar : UtilIO
    {
        //PathGenerico : WORKING_DIRECTORY/USERS/USERID/AVATAR
        //PathSpecifico per le immagini in base alla dimensione: PathGenerico/LG || PathGenerico/MD || PathGenerico/SM || PathGenerico/XS

        private readonly string AVATAR_PATH = ConfigurationManager.AppSettings["AvatarPath"];
        private readonly string USERS_PATH = "USERS";
        private readonly string LG = "LG";
        private readonly string MD = "MD";
        private readonly string SM = "SM";
        private readonly string XS = "XS";

        public Avatar() : base()
        {
        }
        public Avatar(string id) : base()
        {
            try
            {
                this.userId = id;
                createUserDirectory();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void createImagesDirectory(AvatarImages avatarImage)
        {
            try
            {
                string sizeDirectory = WORKING_DIRECTORY + Path.DirectorySeparatorChar + USERS_PATH + Path.DirectorySeparatorChar + userId + Path.DirectorySeparatorChar + AVATAR_PATH + Path.DirectorySeparatorChar + avatarImage.size;
                Directory.CreateDirectory(sizeDirectory);
                File.WriteAllBytes(sizeDirectory + Path.DirectorySeparatorChar + avatarImage.name, Convert.FromBase64String(avatarImage.data));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void createAvatarDirectory()
        {
            try
            {
                string avatarDirectory = WORKING_DIRECTORY + Path.DirectorySeparatorChar + USERS_PATH + Path.DirectorySeparatorChar + userId + Path.DirectorySeparatorChar + AVATAR_PATH;
                if (Directory.Exists(avatarDirectory))
                    Directory.Delete(avatarDirectory, true);
                Directory.CreateDirectory(avatarDirectory);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AvatarImages> getAvatarByUser(string id, string fileSize)
        {
            List<AvatarImages> oAvatarList = new List<AvatarImages>();
            try
            {
                this.userId = id;
                string avatarDirectory = WORKING_DIRECTORY + Path.DirectorySeparatorChar + USERS_PATH + Path.DirectorySeparatorChar + userId + Path.DirectorySeparatorChar + AVATAR_PATH;

                if (Directory.Exists(avatarDirectory))
                {
                    string[] subdirectoryEntries = Directory.GetDirectories(avatarDirectory);
                    foreach (string subdirectory in subdirectoryEntries)
                    {
                        AvatarImages oAvatar = new AvatarImages();
                        oAvatar.size = new DirectoryInfo(subdirectory).Name;
                        if (fileSize != null && (fileSize == LG || fileSize == MD || fileSize == SM || fileSize == XS))
                        {
                            if (fileSize != oAvatar.size)
                            {
                                continue;
                            }
                        }

                        string[] fileEntries = Directory.GetFiles(subdirectory);
                        foreach (string fileName in fileEntries)
                        {
                            oAvatar.name = Path.GetFileName(fileName);
                            //oAvatar.size = new FileInfo(fileName).Length.ToString();
                            oAvatar.type = MimeMapping.GetMimeMapping(fileName);
                            oAvatar.data = Convert.ToBase64String(File.ReadAllBytes(fileName));

                            oAvatarList.Add(oAvatar);
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return oAvatarList;
        }
        public List<AvatarImages> getAvatarByUser(string id)
        {
            return getAvatarByUser(id, null);
        }
    }
}