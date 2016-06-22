[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AluminiWebApp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(AluminiWebApp.App_Start.NinjectWebCommon), "Stop")]

namespace AluminiWebApp.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using AluminiService.Interfaces;
    using AluminiService;
    using AluminiRepository.Interfaces;
    using AluminiRepository;
    using Alumini.Logger;
    using AluminiRepository.Factories;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            kernel.Bind<Alumini.Logger.ILogger>().To(typeof(Logger));
            kernel.Bind<IDbConnectionFactory>().To(typeof(AluminiDatabaseContextFactory)).WithConstructorArgument("connectionString", "");

            kernel.Bind<ISaluationService>().To(typeof(SaluationService));
            kernel.Bind<ISaluationRepository>().To(typeof(SaluationRepository));

            kernel.Bind<ICourseCategoryService>().To(typeof(CourseCategoriesService));
            kernel.Bind<ICourseCategoryRepository>().To(typeof(CourseCategoryRepository));

            kernel.Bind<ICourseService>().To(typeof(CoursesService));
            kernel.Bind<ICoursesRepository>().To(typeof(CoursesRepository));

            kernel.Bind<IGraduationYearService>().To(typeof(GraduationYearService));
            kernel.Bind<IGraduationYearsRepository>().To(typeof(GraduationYearsRepository));


            kernel.Bind<IUserInfoService>().To(typeof(UserInfoService));
            kernel.Bind<IUserInfoRepository>().To(typeof(UserInfoRepository));

            kernel.Bind<IStateDistrictCityService>().To(typeof(StateDistrictCityService));
            kernel.Bind<IStateDistrictCityRepository>().To(typeof(StateDistrictCityRepository));

            kernel.Bind<IEducationalDetailRepository>().To(typeof(EducationalDetailRepository));
            kernel.Bind<IEducationalDetailService>().To(typeof(EducationalDetailService));

            kernel.Bind<IFacultyWorkInfoRepository>().To(typeof(FacultyWorkInfoRepository));
            kernel.Bind<IFacultyWorkInfoService>().To(typeof(FacultyWorkInfoService));

            kernel.Bind<IProfessionalDetailsRepository>().To(typeof(ProfessionalDetailsRepository));
            kernel.Bind<IProfessionalDetailsService>().To(typeof(ProfessionalDetailsService));

            kernel.Bind<IGenericMethodsRepository>().To(typeof(GenericMethods));
            kernel.Bind<IGenericMethodsService>().To(typeof(GenericMethodsService));

            kernel.Bind<IUserDetailsViewRepository>().To(typeof(UserDetailsViewRepository));
            kernel.Bind<IUserDetailsViewService>().To(typeof(UserDetailsViewService));

            kernel.Bind<IEventCategoryRepository>().To(typeof(EventCategorysRepository));
            kernel.Bind<IEventCategoryService>().To(typeof(EventCategoryService));

            kernel.Bind<IUserPostRepository>().To(typeof(UserPostsRepository));
            kernel.Bind<IUserPostService>().To(typeof(UserPostService));


            kernel.Bind<IUserPostPicturesRepository>().To(typeof(UserPostsPicturesRepository));
            kernel.Bind<IUserPostPicturesService>().To(typeof(UserPostPicturesService));

            kernel.Bind<IUserPostVisibleRepository>().To(typeof(UserPostVisibleRepository));
            kernel.Bind<IUserPostVisibleService>().To(typeof(UserPostVisibleServices));

            kernel.Bind<IEventsRepository>().To(typeof(EventRepositoy));
            kernel.Bind<IEventService>().To(typeof(EventService));

            kernel.Bind<ITicketTypesRepository>().To(typeof(EventTicketTypesRepository));
            kernel.Bind<IEventTicketTypesServices>().To(typeof(EventTicketTypesServices));

            kernel.Bind<IUserSelectionEventsRepository>().To(typeof(UserSelectionEventsRepository));
            kernel.Bind<IUserSelectionEventsService>().To(typeof(UserselectionEventsService));


            kernel.Bind<IuserEventBookingsRepository>().To(typeof(UserBookingEventsRepository));
            kernel.Bind<IUserselectionBookingsService>().To(typeof(UserSelectionBookingsService));

            kernel.Bind<IUserPaymentsRepository>().To(typeof(UserPaymentRepository));
            kernel.Bind<IUserPaymentService>().To(typeof(UserPaymentsService));


            kernel.Bind<IUser_JobPostingsREpository>().To(typeof(User_JobPostingRepository));
            kernel.Bind<IUser_JobPostingService>().To(typeof(User_jobPostingServices));

            kernel.Bind<INewsRoomRepository>().To(typeof(NewsRoomRepositroy));
            kernel.Bind<INewsRoomService>().To(typeof(NewsRoomService));


            kernel.Bind<IMemoriesRepository>().To(typeof(MemoriesRepository));
            kernel.Bind<IMemoriesServices>().To(typeof(MemoriesServices));

            kernel.Bind<IDonationRepository>().To(typeof(DonationRepository));
            kernel.Bind<IDonationService>().To(typeof(DonationService));

            kernel.Bind<IAlbumGalleryRepository>().To(typeof(AlbumGalleryRepository));
            kernel.Bind<IAlbumGalleryService>().To(typeof(AlbumGalleryService));
        }
    }
}
