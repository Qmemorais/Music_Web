using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface IArtistService
    {
        public void CreateArtist(ArtistCreateDto artistToCreate);
        public void DeleteArtist(int artistId);
        public void UpdateArtist(int artistId, ArtistUpdateDto artistToUpdate);
        public IEnumerable<ArtistDto> GetAllArtists();
        public ArtistDto GetArtist(int artistId);
    }
}
