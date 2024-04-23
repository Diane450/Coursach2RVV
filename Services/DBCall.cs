using kursachRVV.Models;
using kursachRVV.ModelsDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursachRVV.Services
{
    public static class DBCall
    {
        private static Ispr2438RomashkinaVvKyrsovoiContext _dbContext { get; set; } = new Ispr2438RomashkinaVvKyrsovoiContext();

        public static async Task<Vhod> Authorize(string login, string password)
        {
            return await _dbContext.Vhods
                .Include(x => x.TexOtNavigation)
                .ThenInclude(x=>x.IspolnitelTexOtSotrydnikNavigations)
                .FirstAsync(u => u.Login == login && u.Password == password);
        }

        public static async Task<List<ZayavkiDTO>> GetAllZayavki()
        {
            return await _dbContext.Zayavkis
                .Include(x => x.IspolnitelNavigation)
                .ThenInclude(x => x.TexOtSotrydnikNavigation)
                .Include(x => x.StastusNavigation)
                .Include(x => x.SrochnostNavigation)
                .Select(x => new ZayavkiDTO()
                {
                    IdZayavki = x.IdZayavki,
                    Opisanie = x.Opisanie,
                    Srochnost = x.SrochnostNavigation,
                    Raspolozenie = x.Raspolozenie,
                    DateAndTime = x.DateAndTime,
                    Status = x.StastusNavigation,
                    Ispolnitel = x.IspolnitelNavigation
                }).ToListAsync();
        }

        public static async Task<ObservableCollection<Srochnost>> GetAllSrochnosts()
        {
            return new ObservableCollection<Srochnost>(await _dbContext.Srochnosts.ToListAsync());
        }
        public static async Task<ObservableCollection<Status>> GetAllStatuses()
        {
            return new ObservableCollection<Status>(await _dbContext.Statuses.ToListAsync());
        }

        public static async Task<ObservableCollection<Ispolnitel>> GetAllIspolnitels()
        {
            return new ObservableCollection<Ispolnitel>(await _dbContext.Ispolnitels
                .Include(x => x.TexOtSotrydnikNavigation).ToListAsync());
        }

        public static async Task SaveZayavkaChanges(ZayavkiDTO zayavkiDTO, Ispolnitel ispolnitel, Status status)
        {
            var zayavka = await _dbContext.Zayavkis.FirstAsync(x => x.IdZayavki == zayavkiDTO.IdZayavki);
            zayavka.StastusNavigation = status;
            zayavka.IspolnitelNavigation = ispolnitel;
            await _dbContext.SaveChangesAsync();
        }

        public static async Task SaveZayavkaChanges(ZayavkiDTO zayavkiDTO, Status status, Vhod user)
        {
            var zayavka = await _dbContext.Zayavkis.FirstAsync(x => x.IdZayavki == zayavkiDTO.IdZayavki);
            zayavka.StastusNavigation = status;
            zayavka.IspolnitelNavigation = user.TexOtNavigation.IspolnitelIdIspolnitelNavigation;
            await _dbContext.SaveChangesAsync();
        }
    }
}
