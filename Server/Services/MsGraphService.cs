﻿using ImageMagick;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreFeatures.Toggles.Server.Services
{
    public class MsGraphService
    {
        private readonly GraphServiceClient _graphServiceClient;

        public MsGraphService(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }

        public async Task<User?> GetGraphApiUser()
        {
            var user = await _graphServiceClient.Me
                .GetAsync(b => b.Options.WithScopes("User.ReadBasic.All", "user.read"));

            return user;
        }

        public async Task<string> GetGraphApiProfilePhoto(string oid)
        {
            var photo = string.Empty;
            byte[] photoByte;

            var streamPhoto = new MemoryStream();
            using (var photoStream = await _graphServiceClient.Users[oid].Photo
                .Content.GetAsync())
            {
                photoStream!.CopyTo(streamPhoto);
                photoByte = streamPhoto!.ToArray();
            }

            using var imageFromFile = new MagickImage(photoByte);
            // Sets the output format to jpeg
            imageFromFile.Format = MagickFormat.Jpeg;
            var size = new MagickGeometry(400, 400);

            // This will resize the image to a fixed size without maintaining the aspect ratio.
            // Normally an image will be resized to fit inside the specified size.
            //size.IgnoreAspectRatio = true;

            imageFromFile.Resize(size);

            // Create byte array that contains a jpeg file
            var data = imageFromFile.ToByteArray();
            photo = Base64UrlEncoder.Encode(data);

            return photo;
        }
    }
}

