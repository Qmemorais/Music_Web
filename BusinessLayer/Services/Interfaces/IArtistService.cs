using System;
using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Services.Interfaces
{
    public interface IArtistService
    {
        public void CreateArtist(ArtistCreateDTO artistCreate);
        public void UpdateArtist(ArtistUpdateDTO artistUpdate, Guid artistId);
        public void DeleteArtist(Guid id);
        public List<ArtistDTO> GetArtists();
        public ArtistDTO GetArtistById(Guid id);
        public List<ArtistDTO> GetAllArtistsByCountry(string country);
    }
}
