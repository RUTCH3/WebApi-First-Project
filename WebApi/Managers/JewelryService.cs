using WebApi.Models;
using WebApi.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Managers
{
    public class JewelryService : IJewelryService
    {
        List<Jewelry> Jewelrys { get; }
        int nextId = 3;
        public JewelryService()
        {
            Jewelrys =
            [
                new Jewelry { Id = 1, Name = "Ring", Weight = 12 },
                new Jewelry { Id = 2, Name = "Necklace", Weight = 12  },
                new Jewelry { Id = 3, Name = "Bracelet", Weight = 12  },
                new Jewelry { Id = 4, Name = "Earrings", Weight = 12  },
                new Jewelry { Id = 5, Name = "Pendant", Weight = 12  },
                new Jewelry { Id = 6, Name = "Brooch", Weight = 12  },
                new Jewelry { Id = 7, Name = "Tiara", Weight = 12  },
                new Jewelry { Id = 8, Name = "Bangle", Weight = 12  },
                new Jewelry { Id = 9, Name = "Crown", Weight = 12  },
                new Jewelry { Id = 10, Name = "Locket", Weight = 12  }
            ];
        }

        public List<Jewelry> GetAll() => Jewelrys;

        public Jewelry Get(int id) => Jewelrys.FirstOrDefault(p => p.Id == id);

        public void Add(Jewelry Jewelry)
        {
            Jewelry.Id = nextId++;
            Jewelrys.Add(Jewelry);
        }

        public void Delete(int id)
        {
            var Jewelry = Get(id);
            if (Jewelry is null)
                return;
            Jewelrys.Remove(Jewelry);
        }

        public void Update(Jewelry Jewelry)
        {
            var index = Jewelrys.FindIndex(p => p.Id == Jewelry.Id);
            if (index == -1)
                return;
            Jewelrys[index] = Jewelry;
        }

        public int Count { get => Jewelrys.Count(); }
    }
}