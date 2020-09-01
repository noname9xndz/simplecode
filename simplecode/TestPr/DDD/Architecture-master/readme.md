# Architecture

![](https://dev.azure.com/rafaelfgx/Architecture/_apis/build/status/Build)
![](https://app.codacy.com/project/badge/Grade/3d1ea5b1f4b745488384c744cb00d51e)
![](https://img.shields.io/github/repo-size/rafaelfgx/Architecture?label=Size)

This project is an example of architecture using new technologies and best practices.

The goal is to share knowledge and use it as reference for new projects.

Thanks for enjoying!

## Technologies

* [.NET Core 3.1](https://dotnet.microsoft.com/download)
* [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core)
* [Entity Framework Core 3.1](https://docs.microsoft.com/en-us/ef/core)
* [C# 8.0](https://docs.microsoft.com/en-us/dotnet/csharp)
* [Angular 10](https://angular.io/docs)
* [UIkit](https://getuikit.com/docs/introduction)
* [Docker](https://docs.docker.com)
* [Azure DevOps](https://dev.azure.com)

## Practices

* Clean Code
* SOLID Principles
* DDD (Domain-Driven Design)
* Separation of Concerns
* DevOps
* Code Analysis

## Run

<details>
<summary>Command Line</summary>

#### Prerequisites

* [.NET Core SDK](https://aka.ms/dotnet-download)
* [SQL Server](https://go.microsoft.com/fwlink/?linkid=866662)
* [Node.js](https://nodejs.org)
* [Angular CLI](https://cli.angular.io)

#### Steps

1. Open directory **source\Web\Frontend** in command line and execute **npm run restore**.
2. Open directory **source\Web** in command line and execute **dotnet run**.
3. Open <https://localhost:8090>.

</details>

<details>
<summary>Visual Studio Code</summary>

#### Prerequisites

* [.NET Core SDK](https://aka.ms/dotnet-download)
* [SQL Server](https://go.microsoft.com/fwlink/?linkid=866662)
* [Visual Studio Code](https://code.visualstudio.com)
* [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)
* [Node.js](https://nodejs.org)
* [Angular CLI](https://cli.angular.io)

#### Steps

1. Open directory **source\Web\Frontend** in command line and execute **npm run restore**.
2. Open **source** directory in Visual Studio Code.
3. Press **F5**.

</details>

<details>
<summary>Visual Studio</summary>

#### Prerequisites

* [Visual Studio](https://visualstudio.microsoft.com)
* [Node.js](https://nodejs.org)
* [Angular CLI](https://cli.angular.io)

#### Steps

1. Open directory **source\Web\Frontend** in command line and execute **npm run restore**.
2. Open **source\Architecture.sln** in Visual Studio.
3. Set **Architecture.Web** as startup project.
4. Press **F5**.

</details>

<details>
<summary>Docker</summary>

#### Prerequisites

* [Docker](https://www.docker.com/get-started)

#### Steps

1. Execute **docker-compose up --build -d** in root directory.
2. Open <http://localhost:8090>.

</details>

## Utils

<details>
<summary>Books</summary>

* **Clean Code: A Handbook of Agile Software Craftsmanship** - Robert C. Martin (Uncle Bob)
* **Clean Architecture: A Craftsman's Guide to Software Structure and Design** - Robert C. Martin (Uncle Bob)
* **Implementing Domain-Driven Design** - Vaughn Vernon
* **Domain-Driven Design Distilled** - Vaughn Vernon
* **Domain-Driven Design: Tackling Complexity in the Heart of Software** - Eric Evans
* **Domain-Driven Design Reference: Definitions and Pattern Summaries** - Eric Evans

</details>

<details>
<summary>Tools</summary>

* [Visual Studio](https://visualstudio.microsoft.com)
* [Visual Studio Code](https://code.visualstudio.com)
* [SQL Server](https://www.microsoft.com/sql-server)
* [Node.js](https://nodejs.org)
* [Angular CLI](https://cli.angular.io)
* [Postman](https://www.getpostman.com)
* [Codacy](https://codacy.com)
* [StackBlitz](https://stackblitz.com)

</details>

<details>
<summary>Visual Studio Extensions</summary>

* [CodeMaid](https://marketplace.visualstudio.com/items?itemName=SteveCadwallader.CodeMaid)
* [ReSharper](https://www.jetbrains.com/resharper)

</details>

<details>
<summary>Visual Studio Code Extensions</summary>

* [Angular Language Service](https://marketplace.visualstudio.com/items?itemName=Angular.ng-template)
* [Angular Snippets](https://marketplace.visualstudio.com/items?itemName=johnpapa.Angular2)
* [Atom One Dark Theme](https://marketplace.visualstudio.com/items?itemName=akamud.vscode-theme-onedark)
* [Bracket Pair Colorizer](https://marketplace.visualstudio.com/items?itemName=CoenraadS.bracket-pair-colorizer)
* [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
* [Debugger for Chrome](https://marketplace.visualstudio.com/items?itemName=msjsdiag.debugger-for-chrome)
* [Material Icon Theme](https://marketplace.visualstudio.com/items?itemName=PKief.material-icon-theme)
* [Sort Lines](https://marketplace.visualstudio.com/items?itemName=Tyriar.sort-lines)
* [TSLint](https://marketplace.visualstudio.com/items?itemName=ms-vscode.vscode-typescript-tslint-plugin)
* [Visual Studio Keymap](https://marketplace.visualstudio.com/items?itemName=ms-vscode.vs-keybindings)

</details>

## Nuget Packages

**Source:** [https://github.com/rafaelfgx/DotNetCore](https://github.com/rafaelfgx/DotNetCore)

**Published:** [https://www.nuget.org/profiles/rafaelfgx](https://www.nuget.org/profiles/rafaelfgx)

## Layers

**Web:** API and Frontend (Angular).

**Application:** Flow control.

**Domain:** Business rules and domain logic.

**Model:** Data transfer objects.

**Database:** Database persistence.

## Web

### Angular

### Service

```typescript
@Injectable({ providedIn: "root" })
export class AppCustomerService {
    constructor(
        private readonly http: HttpClient,
        private readonly gridService: GridService) { }

    private readonly url = "customers";

    add(model: CustomerModel) {
        return this.http.post<number>(this.url, model);
    }

    delete(id: number) {
        return this.http.delete(`${this.url}/${id}`);
    }

    get(id: number) {
        return this.http.get<CustomerModel>(`${this.url}/${id}`);
    }

    grid(parameters: GridParametersModel) {
        return this.gridService.get<CustomerModel>(`${this.url}/grid`, parameters);
    }

    inactivate(id: number) {
        return this.http.patch(`${this.url}/${id}/inactivate`, {});
    }

    list() {
        return this.http.get<CustomerModel[]>(this.url);
    }

    update(model: CustomerModel) {
        return this.http.put(`${this.url}/${model.id}`, model);
    }
}
```

### Guard

```typescript
@Injectable({ providedIn: "root" })
export class AppGuard implements CanActivate {
    constructor(private readonly appAuthService: AppAuthService) { }

    canActivate() {
        if (this.appAuthService.authenticated()) { return true; }
        this.appAuthService.signin();
        return false;
    }
}
```

### ErrorHandler

```typescript
@Injectable({ providedIn: "root" })
export class AppErrorHandler implements ErrorHandler {
    constructor(private readonly injector: Injector) { }

    handleError(error: any) {
        if (error instanceof HttpErrorResponse) {
            switch (error.status) {
                case 422: {
                    const appModalService = this.injector.get<AppModalService>(AppModalService);
                    appModalService.alert(error.error);
                    return;
                }
            }
        }

        console.error(error);
    }
}
```

### HttpInterceptor

```typescript
@Injectable({ providedIn: "root" })
export class AppHttpInterceptor implements HttpInterceptor {
    constructor(private readonly appAuthService: AppAuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler) {
        request = request.clone({
            setHeaders: { Authorization: `Bearer ${this.appAuthService.token()}` }
        });

        return next.handle(request);
    }
}
```
### ASP.NET Core

### Startup

```csharp
public class Startup
{
    public void Configure(IApplicationBuilder application)
    {
        application.UseException();
        application.UseHttps();
        application.UseRouting();
        application.UseStaticFiles();
        application.UseResponseCompression();
        application.UseAuthentication();
        application.UseAuthorization();
        application.UseEndpoints();
        application.UseSpa();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSecurity();
        services.AddResponseCompression();
        services.AddControllersMvcJsonOptions();
        services.AddSpa();
        services.AddContext();
        services.AddServices();
    }
}
```

### Controller

```csharp
[ApiController]
[Route("customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public Task<IActionResult> AddAsync(CustomerModel model)
    {
        return _customerService.AddAsync(model).ResultAsync();
    }

    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteAsync(long id)
    {
        return _customerService.DeleteAsync(id).ResultAsync();
    }

    [HttpGet("{id}")]
    public Task<IActionResult> GetAsync(long id)
    {
        return _customerService.GetAsync(id).ResultAsync();
    }

    [HttpGet("grid")]
    public Task<IActionResult> GridAsync([FromQuery] GridParameters parameters)
    {
        return _customerService.GridAsync(parameters).ResultAsync();
    }

    [HttpPatch("{id}/inactivate")]
    public Task InactivateAsync(long id)
    {
        return _customerService.InactivateAsync(id);
    }

    [HttpGet]
    public Task<IActionResult> ListAsync()
    {
        return _customerService.ListAsync().ResultAsync();
    }

    [HttpPut("{id}")]
    public Task<IActionResult> UpdateAsync(CustomerModel model)
    {
        return _customerService.UpdateAsync(model).ResultAsync();
    }
}
```

## Application

### Service

```csharp
public sealed class CustomerService : ICustomerService
{
    private readonly ICustomerFactory _customerFactory;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService
    (
        ICustomerFactory customerFactory,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork
    )
    {
        _customerFactory = customerFactory;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<long>> AddAsync(CustomerModel model)
    {
        var validation = await new AddCustomerModelValidator().ValidateAsync(model);

        if (validation.Failed)
        {
            return Result<long>.Fail(validation.Message);
        }

        var customer = _customerFactory.Create(model);

        await _customerRepository.AddAsync(customer);

        await _unitOfWork.SaveChangesAsync();

        return Result<long>.Success(customer.Id);
    }

    public async Task<IResult> DeleteAsync(long id)
    {
        await _customerRepository.DeleteAsync(id);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public Task<CustomerModel> GetAsync(long id)
    {
        return _customerRepository.GetModelAsync(id);
    }

    public Task<Grid<CustomerModel>> GridAsync(GridParameters parameters)
    {
        return _customerRepository.GridAsync(parameters);
    }

    public async Task InactivateAsync(long id)
    {
        var customer = new Customer(id);

        customer.Inactivate();

        await _customerRepository.InactivateAsync(customer);

        await _unitOfWork.SaveChangesAsync();
    }

    public Task<IEnumerable<CustomerModel>> ListAsync()
    {
        return _customerRepository.ListModelAsync();
    }

    public async Task<IResult> UpdateAsync(CustomerModel model)
    {
        var validation = await new UpdateCustomerModelValidator().ValidateAsync(model);

        if (validation.Failed)
        {
            return Result.Fail(validation.Message);
        }

        var customer = _customerFactory.Create(model);

        await _customerRepository.UpdateAsync(customer.Id, customer);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
```

### Factory

```csharp
public class CustomerFactory : ICustomerFactory
{
    public Customer Create(CustomerModel model)
    {
        return new Customer
        (
            new Name(model.Forename, model.Surname),
            new Email(model.Email)
        );
    }
}
```

## Domain

### Entity

```csharp
public class Customer : Entity<long>
{
    public Customer(long id) : base(id) { }

    public Customer
    (
        Name name,
        Email email
    )
    {
        Name = name;
        Email = email;
        Activate();
    }

    public Name Name { get; private set; }

    public Email Email { get; private set; }

    public Status Status { get; private set; }

    public void Activate()
    {
        Status = Status.Active;
    }

    public void Inactivate()
    {
        Status = Status.Inactive;
    }
}
```

### ValueObject

```csharp
public sealed class Name : ValueObject
{
    public Name(string forename, string surname)
    {
        Forename = forename;
        Surname = surname;
    }

    public string Forename { get; }

    public string Surname { get; }

    protected override IEnumerable<object> Equals()
    {
        yield return Forename;
        yield return Surname;
    }
}
```

## Model

### Model

```csharp
public class CustomerModel
{
    public long Id { get; set; }

    public string Forename { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }
}
```

### ModelValidator

```csharp
public abstract class CustomerModelValidator : Validator<CustomerModel>
{
    public CustomerModelValidator Id()
    {
        RuleFor(customer => customer.Id).NotEmpty();
        return this;
    }

    public CustomerModelValidator Forename()
    {
        RuleFor(customer => customer.Forename).NotEmpty();
        return this;
    }

    public CustomerModelValidator Surname()
    {
        RuleFor(customer => customer.Surname).NotEmpty();
        return this;
    }

    public CustomerModelValidator Email()
    {
        RuleFor(customer => customer.Email).EmailAddress();
        return this;
    }
}
```

```csharp
public sealed class AddCustomerModelValidator : CustomerModelValidator
{
    public AddCustomerModelValidator() => Forename().Surname().Email();
}
```

```csharp
public sealed class UpdateCustomerModelValidator : CustomerModelValidator
{
    public UpdateCustomerModelValidator() => Id().Forename().Surname().Email();
}
```

```csharp
public sealed class DeleteCustomerModelValidator : CustomerModelValidator
{
    public DeleteCustomerModelValidator() => Id();
}
```

## Database

### Context

```csharp
public sealed class Context : DbContext
{
    public Context(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        builder.Seed();
    }
}
```

### Configuration

```csharp
public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer), nameof(Customer));
        builder.HasKey(customer => customer.Id);
        builder.Property(customer => customer.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(customer => customer.Status).IsRequired();
        builder.OwnsOne(customer => customer.Name, customerName =>
        {
            customerName.Property(name => name.Forename).HasColumnName(nameof(Name.Forename)).HasMaxLength(100).IsRequired();
            customerName.Property(name => name.Surname).HasColumnName(nameof(Name.Surname)).HasMaxLength(200).IsRequired();
        });
        builder.OwnsOne(customer => customer.Email, customerEmail =>
        {
            customerEmail.Property(email => email.Value).HasColumnName(nameof(User.Email)).HasMaxLength(300).IsRequired();
            customerEmail.HasIndex(email => email.Value).IsUnique();
        });
    }
}
```

### Repository

```csharp
public sealed class CustomerRepository : EFRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(Context context) : base(context) { }

    public Task<CustomerModel> GetModelAsync(long id)
    {
        return Queryable.Where(CustomerExpression.Id(id)).Select(CustomerExpression.Model).SingleOrDefaultAsync();
    }

    public Task<Grid<CustomerModel>> GridAsync(GridParameters parameters)
    {
        return Queryable.Select(CustomerExpression.Model).GridAsync(parameters);
    }

    public Task InactivateAsync(Customer customer)
    {
        return UpdatePartialAsync(customer.Id, new { customer.Status });
    }

    public async Task<IEnumerable<CustomerModel>> ListModelAsync()
    {
        return await Queryable.Select(CustomerExpression.Model).ToListAsync();
    }
}
```

### Expression

```cs
public static class CustomerExpression
{
    public static Expression<Func<Customer, CustomerModel>> Model => customer => new CustomerModel
    {
        Id = customer.Id,
        Forename = customer.Name.Forename,
        Surname = customer.Name.Surname,
        Email = customer.Email.Value
    };

    public static Expression<Func<Customer, bool>> Id(long id)
    {
        return customer => customer.Id == id;
    }
}
```
