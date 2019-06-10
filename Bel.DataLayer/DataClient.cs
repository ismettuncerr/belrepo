using Bel.DataLayer.Interfaces;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer
{
    public class DataClient
    {
        private static Lazy<DataClient> dataClient = new Lazy<DataClient>(() => new DataClient());
        public static DataClient Provider { get { return dataClient.Value; } }

        //private static Lazy<beldatabaseEntities> lazybeldatabaseEntities = new Lazy<beldatabaseEntities>();
        //private static beldatabaseEntities beldatabaseEntities { get { return lazybeldatabaseEntities.Value; } }

           

        private Lazy<IReservationRepository> lazyReservationRepository = new Lazy<IReservationRepository>(() => new ReservationRepository(new beldatabaseEntities()));
        public IReservationRepository ReservationRepository { get { return lazyReservationRepository.Value; } }

        private Lazy<IClassHourRepository> lazyClassHourRepository = new Lazy<IClassHourRepository>(() => new ClassHourRepository(new beldatabaseEntities()));
        public IClassHourRepository ClassHourRepository { get { return lazyClassHourRepository.Value; } }

        private Lazy<IMunicipalityClassRepository> lazyMunicipalityClassRepository = new Lazy<IMunicipalityClassRepository>(() => new MunicipalityClassRepository(new beldatabaseEntities()));
        public IMunicipalityClassRepository MunicipalityClassRepository { get { return lazyMunicipalityClassRepository.Value; } }

        private Lazy<ISchoolClassRepository> lazySchoolClassRepository = new Lazy<ISchoolClassRepository>(() => new SchoolClassRepository(new beldatabaseEntities()));
        public ISchoolClassRepository SchoolClassRepository { get { return lazySchoolClassRepository.Value; } }

        private Lazy<IUserRepository> lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(new beldatabaseEntities()));
        public IUserRepository UserRepository { get { return lazyUserRepository.Value; } }

        private Lazy<IUserRoleRepository> lazyUserRoleRepository = new Lazy<IUserRoleRepository>(() => new UserRoleRepository(new beldatabaseEntities()));
        public IUserRoleRepository UserRoleRepository { get { return lazyUserRoleRepository.Value; } }

        private Lazy<INewsRepository> lazyNewsRepository = new Lazy<INewsRepository>(() => new NewsRepository(new beldatabaseEntities()));
        public INewsRepository NewsRepository { get { return lazyNewsRepository.Value; } }

        private Lazy<IGalleryRepository> lazyGalleryRepository = new Lazy<IGalleryRepository>(() => new GalleryRepository(new beldatabaseEntities()));
        public IGalleryRepository GalleryRepository { get { return lazyGalleryRepository.Value; } }

        private Lazy<IAboutRepository> lazyAboutRepository = new Lazy<IAboutRepository>(() => new AboutRepository(new beldatabaseEntities()));
        public IAboutRepository AboutRepository { get { return lazyAboutRepository.Value; } }

        private Lazy<IContactRepository> lazyContactRepository = new Lazy<IContactRepository>(() => new ContactRepository(new beldatabaseEntities()));
        public IContactRepository ContactRepository { get { return lazyContactRepository.Value; } }
    }
}
