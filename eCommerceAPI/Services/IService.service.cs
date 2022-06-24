interface IService
{
    dynamic GetAll();
    dynamic Get(int ID);

    dynamic Create(dynamic model);

    dynamic Update(dynamic model);

    dynamic Delete(dynamic model); 

}