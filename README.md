# Using-Entity-Framework-with-In-Memory-Database
20487_MOD02_DEMO_L4

### Demonstration: Using Entity Framework with In-Memory Database

#### Demonstration Steps

1. Open the Command Prompt window.

2. To change the directory to the starter project, at the command prompt, run the following command:

   ```bash
    cd [Repository Root]\AllFiles\Mod02\DemoFiles\InMemory\Starter\InMemory.Dal.Test
   ```

3. To use **Entity Framework Core InMemory**, install the following package from the command prompt:

   ```base
    dotnet add package Microsoft.EntityFrameworkCore.InMemory --version=2.1.1
   ```

4. To restore all the dependencies and tools of a project, at the command prompt, run the following command:

   ```base
    dotnet restore
   ```

5. Move to the solution folder, paste the following command, and then press Enter:

   ```bash
   cd ..
   ```

6. To open the solution in VS Code, paste the following command, and then press Enter:

   ```bash
    code .
   ```

7. Expand **InMemory.Dal**, expand **Database**, and then click **SchoolContext**.

8. To add an empty constructor to the class, paste the following code:

   ```cs
    public SchoolContext()
    {
   
    }
   ```

9. To add a constructor with the **DbContextOptions\<SchoolContext\>** parameter to the class, paste the following code:

   ```cs
   public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
   {
   }
   ```

10. Locate the **OnConfiguring** method and add the following code:

    ```cs
    if(!optionsBuilder.IsConfigured)
    {
        optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
    }
    ```

    >**Note**: In your tests, you are going to externally configure the context to use the **InMemory** provider. If you are configuring a database provider by overriding **OnConfiguring** in your context, then you need to add some conditional code to ensure that you configure the database provider only if it has not already been configured.    

11. Expand **InMemory.Dal.Test**, and then click **DBInMemoryTest**.

12. Add the following property to the class:

    ```cs
     private DbContextOptions<SchoolContext> _options =
                 new DbContextOptionsBuilder<SchoolContext>()
                     .UseInMemoryDatabase(databaseName: "TestDatabase")
                     .Options;
    ```

13. Add the following **Test Method** to the class:

    ```cs
     [TestMethod]
     public void UpdateTeacherSalaryTest()
     {
         Teacher teacher = new Teacher { Name = "Terry Adams", Salary = 10000 };
         teacher = _teacherRepository.Add(teacher);
         teacher.Salary += 10000;
         teacher = _teacherRepository.Update(teacher);
    
         using (var context = new SchoolContext(_options))
         {
             var result = context.Teachers.FirstOrDefault((s) => s.PersonId == teacher.PersonId);
             Assert.AreEqual(result.Salary, 20000);
         }
     }
    ```

14. Switch to the Command Prompt window. To change directory to the **InMemory.Dal.Test** project, at the command prompt, run the following command:

    ```bash
    cd InMemory.Dal.Test
    ```

15. To run the all the test methods, paste the following command, and then press Enter:

    ```bash
     dotnet test
    ```
    
![20487D_Images](https://github.com/ialcaidef/Using-Entity-Framework-with-In-Memory-Database/blob/master/InMemory.Dal.Test/01.png)

16. Close all open windows.
