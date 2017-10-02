namespace _31_boilerplate_api.Commands
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using _31_boilerplate_api.Repositories;

    public class DeleteCarCommand : IDeleteCarCommand
    {
        private readonly ICarRepository carRepository;

        public DeleteCarCommand(ICarRepository carRepository) =>
            this.carRepository = carRepository;

        public async Task<IActionResult> ExecuteAsync(int carId)
        {
            var car = await this.carRepository.Get(carId);
            if (car == null)
            {
                return new NotFoundResult();
            }

            await this.carRepository.Delete(car);

            return new NoContentResult();
        }
    }
}
