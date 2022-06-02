using BandAPI.Data;
using BandAPI.Entities;
using BandAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BandAPI.Services
{
    public class BandAlbumRepository : IBandAlbumRepository
    {
        private readonly DataContext _db;

        public BandAlbumRepository(DataContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(_db));
        }

        public void AddAlbum(Guid bandId, Album album)
        {
            if (bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));
            if (album == null)
                throw new ArgumentNullException(nameof(album));

            album.BandId = bandId;
            _db.Albums.Add(album);

        }

        public void AddBand(Band band)
        {
            if (band == null)
                throw new ArgumentNullException(nameof(band));

            _db.Bands.Add(band);
        }

        public bool AlbumExists(Guid albumId)
        {
            if (albumId == Guid.Empty)
                throw new ArgumentNullException(nameof(albumId));
            return _db.Albums.Any(x => x.Id == albumId);
        }

        public bool BandExists(Guid bandId)
        {
            if (bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));
            return _db.Bands.Any(x => x.Id == bandId);
        }

        public void DeleteAlbum(Album album)
        {
            if (album.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(album.Id));
            _db.Albums.Remove(album);

        }

        public void DeleteBand(Band band)
        {
            if (band.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(band.Id));
            _db.Bands.Remove(band);
        }

        public Album GetAlbum(Guid bandId, Guid albumId)
        {
            if (bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));
            if (albumId == Guid.Empty)
                throw new ArgumentNullException(nameof(albumId));

            return _db.Albums.Where(x => x.BandId == bandId && x.Id == albumId).FirstOrDefault();
        }

        public IEnumerable<Album> GetAlbums(Guid bandId)
        {
            if (bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));
            return _db.Albums.Where(x => x.BandId == bandId).OrderBy(x => x.Title).ToList();
        }

        public Band GetBand(Guid bandId)
        {
            if (bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));
            return _db.Bands.FirstOrDefault(x => x.Id == bandId);
        }

        public IEnumerable<Band> GetBands()
        {
            return _db.Bands.ToList();
        }

        public IEnumerable<Band> GetBands(IEnumerable<Guid> bandIds)
        {
            if (bandIds == null)
                throw new ArgumentNullException(nameof(bandIds));

            return _db.Bands.Where(x => bandIds.Contains(x.Id)).OrderBy(x => x.Name).ToList();
        }
        public IEnumerable<Band> GetBands(BandResourceParameters param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));

            if (string.IsNullOrEmpty(param.MainGenre) && string.IsNullOrEmpty(param.SearchQuery))
                return GetBands();

            var collection = _db.Bands as IQueryable<Band>;
            if (!string.IsNullOrEmpty(param.MainGenre))
            {
                param.MainGenre = param.MainGenre.Trim();
                collection = collection.Where(x => x.MainGenre == param.MainGenre);

            }
            if (!string.IsNullOrEmpty(param.SearchQuery))
            {
                param.SearchQuery = param.SearchQuery.Trim();

                collection = collection.Where(x => x.Name.Contains(param.SearchQuery));
            }

            return collection.ToList();
        }

        public bool Save()
        {
            return (_db.SaveChanges() >= 0);
        }

        public void UpdateAlbum(Album album)
        {
            throw new NotImplementedException();
        }

        public void UpdateBand(Band band)
        {
            throw new NotImplementedException();
        }
    }
}
