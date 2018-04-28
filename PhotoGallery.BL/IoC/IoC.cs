using Ninject;
using PhotoGallery.DAL;

namespace PhotoGallery.BL.IoC
{
    public static class IoC
    {
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        public const int PageSize = 20;

        public static IKernel Kernel { get; } = new StandardKernel();
        public static IApplicationViewModel Application => IoC.Get<IApplicationViewModel>();
        public static Messenger Messenger => IoC.Get<Messenger>();
        public static IUnitOfWork UnitOfWork => IoC.Get<UnitOfWork>();
        public static IAddPhoto AddPhoto => IoC.Get<IAddPhoto>();

        public static void SetUp()
        {
            BindMessenger(new Messenger());
            BindUnitOfWork(new UnitOfWork(new DataContext()));
        }

        private static void BindMessenger(Messenger messenger)
        {
            Kernel.Bind<Messenger>().ToConstant(messenger);
        }

        private static void BindUnitOfWork(UnitOfWork unitOfWork)
        {
            Kernel.Bind<IUnitOfWork>().ToConstant(unitOfWork);
        }
    }
}