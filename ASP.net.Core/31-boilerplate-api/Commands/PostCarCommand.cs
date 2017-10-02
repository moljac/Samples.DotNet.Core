namespace _31_boilerplate_api.Commands
{
    using System.Threading.Tasks;
    using Boilerplate;
    using Microsoft.AspNetCore.Mvc;
    using _31_boilerplate_api.Constants;
    using _31_boilerplate_api.Repositories;
    using _31_boilerplate_api.ViewModels;

    public class PostCarCommand : IPostCarCommand
    {
        private readonly ICarRepository carRepository;
        private readonly ITranslator<Models.Car, Car> carToCarTranslator;
        private readonly ITranslator<SaveCar, Models.Car> saveCarToCarTranslator;

        public PostCarCommand(
            ICarRepository carRepository,
            ITranslator<Models.Car, Car> carToCarTranslator,
            ITranslator<SaveCar, Models.Car> saveCarToCarTranslator)
        {
            this.carRepository = carRepository;
            this.carToCarTranslator = carToCarTranslator;
            this.saveCarToCarTranslator = saveCarToCarTranslator;
        }

        public async Task<IActionResult> ExecuteAsync(SaveCar saveCar)
        {
            var car = this.saveCarToCarTranslator.Translate(saveCar);
            car = await this.carRepository.Add(car);
            var carViewModel = this.carToCarTranslator.Translate(car);

            return new CreatedAtRouteResult(
                CarsControllerRoute.GetCar,
                new { carId = carViewModel.CarId },
                carViewModel);
        }
    }
}
