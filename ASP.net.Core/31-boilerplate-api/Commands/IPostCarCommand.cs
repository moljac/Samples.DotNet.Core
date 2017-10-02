namespace _31_boilerplate_api.Commands
{
    using Boilerplate.AspNetCore;
    using _31_boilerplate_api.ViewModels;

    public interface IPostCarCommand : IAsyncCommand<SaveCar>
    {
    }
}
