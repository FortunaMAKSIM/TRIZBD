using GLOproject.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GLOproject.Commands
{
    public class SaveCommand : BaseCommand
    {
        private readonly AppDbContext _context;
        public SaveCommand(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public override void Execute(object parameter)
        {
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Сохранение успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex?.InnerException?.Message);
            }
        }
    }
}
