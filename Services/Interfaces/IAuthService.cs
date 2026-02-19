public interface IAuthService
{
    object Register(Person person);
    object Login(LoginDto model);
    List<Person> GetAllPersons();
    string DeletePerson(int id);
}
