namespace _31_boilerplate_api.ViewModelSchemaFilters
{
    using _31_boilerplate_api.ViewModels;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class SaveCarSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            model.Default = new SaveCar()
            {
                Cylinders = 6,
                Make = "Honda",
                Model = "Civic"
            };
        }
    }
}
