using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using KUKULCAN.SharedKernel.Abstractions;
using KUKULCAN.SharedKernel.Abstractions.Capabilities;
using KUKULCAN.SharedKernel.Attributes;
using KUKULCAN.SharedKernel.Collections;
using KUKULCAN.SharedKernel.Domain;
using KUKULCAN.SharedKernel.DomainEvents.Abstractions;
using KUKULCAN.SharedKernel.DomainEvents.Base;
using KUKULCAN.SharedKernel.DomainEvents.Collections;
using KUKULCAN.SharedKernel.DomainEvents.Dispatching;
using KUKULCAN.SharedKernel.Exceptions;
using KUKULCAN.SharedKernel.Globalization.Abstractions;
using KUKULCAN.SharedKernel.Globalization.Culture;
using KUKULCAN.SharedKernel.Globalization.Models;
using KUKULCAN.SharedKernel.Guards;
using KUKULCAN.SharedKernel.Identifiers;
using KUKULCAN.SharedKernel.Identifiers.Interfaces;
using KUKULCAN.SharedKernel.Maybe;
using KUKULCAN.SharedKernel.Results;
using KUKULCAN.SharedKernel.Specifications;
using KUKULCAN.SharedKernel.Time;
using KUKULCAN.SharedKernel.Time.Extensions;
using KUKULCAN.SharedKernel.Validations;
using KUKULCAN.SharedKernel.Versioning;

namespace KUKULCAN.SharedKernel.Client;

/// <summary>
/// Interactive console client used to exercise the public API of KUKULCAN.SharedKernel.
/// </summary>
internal static class Program
{
    private static readonly Dictionary<string, Action> Demos = new()
    {
        ["1"] = DemoGuards,
        ["2"] = DemoVersioning,
        ["3"] = DemoAttributes,
        ["4"] = DemoCollections,
        ["5"] = DemoIdentifiers,
        ["6"] = DemoMaybe,
        ["7"] = DemoResults,
        ["8"] = DemoValidations,
        ["8E"] = DemoExceptions,
        ["9"] = DemoDomain,
        ["10"] = DemoSpecifications,
        ["11"] = DemoDomainEvents,
        ["12"] = DemoTime,
        ["13"] = DemoGlobalization,
        ["14"] = DemoAbstractionsAndCapabilities,
        ["15"] = ShowPublicApiInventory
    };

    public static async Task Main()
    {
        Console.Title = "KUKULCAN.SharedKernel.Client";

        PrintHeader();
        Console.WriteLine("This application is a manual integration/demo client.");
        Console.WriteLine("It exercises real public APIs; it is not a replacement for NUnit tests.");
        Console.WriteLine();

        while (true)
        {
            PrintMenu();
            string? option = Console.ReadLine()?.Trim();

            if (option is "0" or "q" or "Q")
            {
                return;
            }

            if (option == "A" || option == "a")
            {
                foreach (Action demo in Demos.Values)
                {
                    RunDemo(demo);
                }

                Console.WriteLine();
                Console.WriteLine("All demonstrations completed.");
                Pause();
                continue;
            }

            if (Demos.TryGetValue(option ?? string.Empty, out Action? selected))
            {
                RunDemo(selected);
                Pause();
                continue;
            }

            Console.WriteLine("Unknown option.");
        }
    }

    private static void PrintHeader()
    {
        Console.WriteLine("==============================================================");
        Console.WriteLine("              KUKULCAN.SharedKernel.Client");
        Console.WriteLine("==============================================================");
    }

    private static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Select a module:");
        Console.WriteLine("  1. Guards");
        Console.WriteLine("  2. Versioning");
        Console.WriteLine("  3. Attributes");
        Console.WriteLine("  4. Collections");
        Console.WriteLine("  5. Identifiers");
        Console.WriteLine("  6. Maybe");
        Console.WriteLine("  7. Results and Errors");
        Console.WriteLine("  8. Validations");
        Console.WriteLine(" 8E. Exceptions");
        Console.WriteLine("  9. Domain");
        Console.WriteLine(" 10. Specifications");
        Console.WriteLine(" 11. Domain Events");
        Console.WriteLine(" 12. Time");
        Console.WriteLine(" 13. Globalization");
        Console.WriteLine(" 14. Abstractions and Capabilities");
        Console.WriteLine(" 15. Public API inventory");
        Console.WriteLine("  A. Run all demonstrations");
        Console.WriteLine("  0. Exit");
        Console.Write("> ");
    }

    private static void RunDemo(Action demo)
    {
        Console.WriteLine();
        Console.WriteLine(new string('-', 62));
        Console.WriteLine($"Running: {demo.Method.Name}");
        Console.WriteLine(new string('-', 62));

        try
        {
            demo();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"ERROR: {exception.GetType().Name}: {exception.Message}");
        }
    }

    private static void DemoGuards()
    {
        Console.WriteLine($"NotDefault(42) = {Guard.NotDefault(42)}");
        Console.WriteLine($"NotEmpty(Guid) = {Guard.NotEmpty(Guid.NewGuid())}");
        Console.WriteLine($"NotEmpty([1,2,3]) count = {Guard.NotEmpty(new[] { 1, 2, 3 }).Count}");

        Expect<ArgumentException>(() => Guard.NotDefault(0));
        Expect<ArgumentException>(() => Guard.NotEmpty(Guid.Empty));
        Expect<ArgumentException>(() => Guard.NotEmpty(Array.Empty<int>()));
        Expect<ArgumentNullException>(() => Guard.NotEmpty<int>(null!));

        Console.WriteLine("Guard validation paths exercised.");
    }

    private static void DemoVersioning()
    {
        SemanticVersion version = new(1, 2, 3, "beta.1", "build.42");

        Console.WriteLine($"Version: {version}");
        Console.WriteLine($"Major={version.Major}, Minor={version.Minor}, Patch={version.Patch}");
        Console.WriteLine($"Prerelease={version.Prerelease}, BuildMetadata={version.BuildMetadata}");
        Console.WriteLine($"IsPrerelease={version.IsPrerelease}, HasBuildMetadata={version.HasBuildMetadata}");

        SemanticVersion parsed = SemanticVersion.Parse("2.0.0-rc.1+linux");
        Console.WriteLine($"Parse(string): {parsed}");

        SemanticVersion parsedWithProvider = SemanticVersion.Parse("2.1.0", CultureInfo.InvariantCulture);
        Console.WriteLine($"Parse(string, provider): {parsedWithProvider}");

        SemanticVersion.TryParse("3.0.0", out SemanticVersion? tryParsed);
        Console.WriteLine($"TryParse(string): {tryParsed}");

        SemanticVersion.TryParse("not-a-version", CultureInfo.InvariantCulture, out _);
        Console.WriteLine("TryParse(invalid) returned false.");

        SemanticVersion spanParsed = SemanticVersion.Parse("4.5.6".AsSpan(), null);
        Console.WriteLine($"Parse(span): {spanParsed}");

        SemanticVersion.TryParse("7.8.9".AsSpan(), null, out SemanticVersion? spanTry);
        Console.WriteLine($"TryParse(span): {spanTry}");

        Console.WriteLine($"CompareTo: {version.CompareTo(parsed)}");
        Console.WriteLine($"Equality: {version == new SemanticVersion(1, 2, 3, "beta.1", "build.42")}");
        Expect<ArgumentOutOfRangeException>(() => new SemanticVersion(-1, 0, 0));
        Expect<ArgumentException>(() => new SemanticVersion(1, 0, 0, "bad value"));
        Expect<FormatException>(() => SemanticVersion.Parse("invalid"));

        Console.WriteLine("SemanticVersion construction, parsing, comparison and flags exercised.");
    }

    private static void DemoAttributes()
    {
        PropertyInfo property = typeof(AttributeDemo).GetProperty(nameof(AttributeDemo.Name))!;

        ValueObjectMemberAttribute? member =
            property.GetCustomAttribute<ValueObjectMemberAttribute>();
        EqualityOrderAttribute? order =
            property.GetCustomAttribute<EqualityOrderAttribute>();

        IgnoreEqualityAttribute? ignored =
            typeof(AttributeDemo).GetProperty(nameof(AttributeDemo.Ignored))!
                .GetCustomAttribute<IgnoreEqualityAttribute>();

        Console.WriteLine($"ValueObjectMember present: {member is not null}");
        Console.WriteLine($"EqualityOrder present: {order is not null}");
        Console.WriteLine($"IgnoreEquality present: {ignored is not null}");
    }

    private static void DemoCollections()
    {
        PageRequest request = new(2, 10);
        Page<int> page = new([11, 12, 13], request.PageNumber, request.PageSize);
        PagedList<int> paged = new(page, 23);

        Console.WriteLine($"PageRequest: page={request.PageNumber}, size={request.PageSize}");
        Console.WriteLine($"Page: count={page.Count}, hasItems={page.HasItems}, isEmpty={page.IsEmpty}");
        Console.WriteLine($"PagedList: total={paged.TotalCount}, totalPages={paged.TotalPages}");
        Console.WriteLine($"Previous={paged.HasPreviousPage}, next={paged.HasNextPage}, first={paged.IsFirstPage}, last={paged.IsLastPage}");

        Expect<ArgumentOutOfRangeException>(() => new PageRequest(0, 10));
        Expect<ArgumentOutOfRangeException>(() => new PageRequest(1, PageRequest.MaximumPageSize + 1));
        Expect<ArgumentNullException>(() => new Page<int>(null!, 1, 10));
        Expect<ArgumentException>(() => new PagedList<int>(page, 2));

        Console.WriteLine($"DefaultPageSize={PageRequest.DefaultPageSize}, MaximumPageSize={PageRequest.MaximumPageSize}");
    }

    private static void DemoIdentifiers()
    {
        IntId intId = new(10);
        LongId longId = new(20);
        GuidId guidId = new(Guid.NewGuid());
        StringId stringId = new("customer-001");

        Console.WriteLine($"IntId: {intId.Value}");
        Console.WriteLine($"LongId: {longId.Value}");
        Console.WriteLine($"GuidId: {guidId.Value}");
        Console.WriteLine($"StringId: {stringId.Value}");

        Console.WriteLine($"Int equality: {intId == new IntId(10)}");
        Console.WriteLine($"Long equality: {longId == new LongId(20)}");
        Console.WriteLine($"Guid equality: {guidId == new GuidId(guidId.Value)}");
        Console.WriteLine($"String equality: {stringId == new StringId("customer-001")}");

        Expect<ArgumentOutOfRangeException>(() => new IntId(0));
        Expect<ArgumentOutOfRangeException>(() => new LongId(0));
        Expect<ArgumentException>(() => new GuidId(Guid.Empty));
        Expect<ArgumentException>(() => new StringId(" "));

        IIdGenerator<IntId> generator = new IntIdGenerator();
        Console.WriteLine($"Generated ID: {generator.New()}");
    }

    private static void DemoMaybe()
    {
        Maybe<int> none = Maybe<int>.None;
        Maybe<int> some = 42;

        Console.WriteLine($"None: HasValue={none.HasValue}, HasNoValue={none.HasNoValue}, ToString={none}");
        Console.WriteLine($"Some: HasValue={some.HasValue}, Value={some.Value}, ToString={some}");

        Console.WriteLine($"TryGetValue(Some): {some.TryGetValue(out int someValue)} -> {someValue}");
        Console.WriteLine($"TryGetValue(None): {none.TryGetValue(out int noneValue)} -> {noneValue}");
        Console.WriteLine($"GetValueOr(None, 99): {none.GetValueOr(99)}");
        Console.WriteLine($"GetValueOr(Some, 99): {some.GetValueOr(99)}");
        Console.WriteLine($"GetValueOr(factory): {none.GetValueOr(() => 123)}");
        Console.WriteLine($"Equality: {some == (Maybe<int>)42}");
        Console.WriteLine($"Inequality: {some != none}");

        Expect<InvalidOperationException>(() => _ = none.Value);
        Expect<ArgumentNullException>(() => none.GetValueOr((Func<int>)null!));
    }

    private static void DemoResults()
    {
        Error custom = new("CLIENT.DEMO", "A client-generated error.");
        Result success = Result.Success();
        Result failure = Result.Failure(custom);
        Result<int> valueSuccess = Result<int>.Success(123);
        Result<int> valueFailure = Result<int>.Failure(custom);

        Console.WriteLine($"Error.None: {Error.None}");
        Console.WriteLine($"Error: code={custom.Code}, description={custom.Description}, text={custom}");
        Console.WriteLine($"Common error codes: {string.Join(", ", typeof(CommonErrorCodes).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Where(f => f.IsLiteral).Select(f => $"{f.Name}={f.GetRawConstantValue()}"))}");
        Console.WriteLine($"Success: IsSuccess={success.IsSuccess}, IsFailure={success.IsFailure}, Error={success.Error}");
        Console.WriteLine($"Failure: IsSuccess={failure.IsSuccess}, IsFailure={failure.IsFailure}, Error={failure.Error}");
        Console.WriteLine($"Result<int> success value={valueSuccess.Value}");
        Console.WriteLine($"Result<int> failure={valueFailure}");

        Console.WriteLine($"Common NotFound: {CommonErrors.NotFound("Customer")}");
        Console.WriteLine($"Common Conflict: {CommonErrors.Conflict("Customer")}");
        Console.WriteLine($"Common InvalidOperation: {CommonErrors.InvalidOperation("Update")}");
        Console.WriteLine($"Common NotSupported: {CommonErrors.NotSupported("Demo")}");
        Console.WriteLine($"Common Unauthorized: {CommonErrors.Unauthorized()}");
        Console.WriteLine($"Common Forbidden: {CommonErrors.Forbidden()}");
        Console.WriteLine($"Common Timeout: {CommonErrors.Timeout()}");
        Console.WriteLine($"Common Cancelled: {CommonErrors.Cancelled()}");
        Console.WriteLine($"Common Unexpected: {CommonErrors.Unexpected()}");
        Console.WriteLine($"Common Unknown: {CommonErrors.Unknown()}");

        Expect<ArgumentNullException>(() => Result.Failure(null!));
        Expect<InvalidOperationException>(() => _ = valueFailure.Value);

        DemonstrateValidationErrors();
    }

    private static void DemonstrateValidationErrors()
    {
        string p = "Age";

        Error[] errors =
        [
            ValidationErrors.ValidationFailed(),
            ValidationErrors.Required(p),
            ValidationErrors.Null(p),
            ValidationErrors.Empty(p),
            ValidationErrors.MinLength(p, 2),
            ValidationErrors.MaxLength(p, 20),
            ValidationErrors.ExactLength(p, 8),
            ValidationErrors.GreaterThan(p, 18),
            ValidationErrors.GreaterThanOrEqual(p, 18),
            ValidationErrors.LessThan(p, 65),
            ValidationErrors.LessThanOrEqual(p, 65),
            ValidationErrors.Between(p, 18, 65),
            ValidationErrors.InvalidFormat(p),
            ValidationErrors.InvalidPattern(p),
            ValidationErrors.InvalidEmail(p),
            ValidationErrors.InvalidPhone(p),
            ValidationErrors.InvalidUrl(p),
            ValidationErrors.PastDate(p),
            ValidationErrors.FutureDate(p),
            ValidationErrors.CollectionMustBeEmpty(p),
            ValidationErrors.CollectionMustNotBeEmpty(p),
            ValidationErrors.Duplicate(p),
            ValidationErrors.InvalidCollectionItem(p),
            ValidationErrors.InvalidEnum(p)
        ];

        foreach (Error error in errors)
        {
            Console.WriteLine($"{error.Code} -> {error.Description}");
        }
    }

    private static void DemoValidations()
    {
        ValidationFailure failure =
            new("Email", "VALIDATION.PATTERN.EMAIL", "Email is invalid.", "bad");

        Console.WriteLine($"Failure: {failure}");
        Console.WriteLine($"Property={failure.PropertyName}, Code={failure.ErrorCode}, Severity={failure.Severity}");

        ValidationResult failed = failure.ToValidationResult();
        ValidationResult many = new[] { failure }.ToValidationResult();

        Console.WriteLine($"Failed IsValid={failed.IsValid}, Failures={failed.Failures.Count}");
        Console.WriteLine($"Many IsValid={many.IsValid}, Failures={many.Failures.Count}");
        Console.WriteLine($"Success IsValid={ValidationResult.Success.IsValid}");
        Console.WriteLine($"Severity values={string.Join(", ", Enum.GetNames<ValidationSeverity>())}");

        Result result = failed.ToResult();
        Console.WriteLine($"Validation -> Result: {result}");

        try
        {
            failed.ThrowIfInvalid();
        }
        catch (ValidationException exception)
        {
            Console.WriteLine($"ThrowIfInvalid produced {exception.GetType().Name} with {exception.Failures.Count} failure(s).");
        }

        Expect<ArgumentException>(() => ValidationResult.Failure(Array.Empty<ValidationFailure>()));
        Expect<ArgumentNullException>(() => ((IEnumerable<ValidationFailure>)null!).ToValidationResult());
        Expect<ArgumentNullException>(() => ((ValidationResult)null!).ThrowIfInvalid());
    }

    private static void DemoExceptions()
    {
        Error error = CommonErrors.NotFound("Customer");

        SharedKernelException[] exceptions =
        [
            new ConflictException(error),
            new UnexpectedException(error),
            new DomainException(error),
            new ForbiddenException(error),
            new NotFoundException(error),
            new ConcurrencyException(error),
            new UnauthorizedException(error)
        ];

        foreach (SharedKernelException exception in exceptions)
        {
            Console.WriteLine($"{exception.GetType().Name}: Error={exception.Error.Code}");
        }

        ValidationFailure failure =
            new("Name", CommonErrorCodes.Required, "Name is required.");

        ValidationResult validation = failure.ToValidationResult();
        ValidationException validationException = new(validation);

        Console.WriteLine($"ValidationException: failures={validationException.Failures.Count}");
        Console.WriteLine($"ValidationException.ValidationResult.IsValid={validationException.ValidationResult.IsValid}");

        Exception inner = new InvalidOperationException("Inner");
        ConflictException withInner = new(error, inner);
        Console.WriteLine($"Inner exception preserved={ReferenceEquals(withInner.InnerException, inner)}");

        Console.WriteLine($"SharedKernelException base type={typeof(SharedKernelException).BaseType?.Name}");
    }

    private static void DemoDomain()
    {
        CustomerId id = new(Guid.NewGuid());
        Customer customer = new(id, "Alice");
        Customer sameId = new(id, "Another Name");

        Console.WriteLine($"Entity: {customer}");
        Console.WriteLine($"Id={customer.Id}, Name={customer.Name}");
        Console.WriteLine($"Same entity type/id equality={customer == sameId}");
        Console.WriteLine($"Different entity equality={customer == new Customer(new CustomerId(Guid.NewGuid()), "Alice")}");

        Money money = new(100m, "EUR");
        Money sameMoney = new(100m, "EUR");
        Money differentMoney = new(120m, "EUR");

        Console.WriteLine($"ValueObject: {money}");
        Console.WriteLine($"Equal={money == sameMoney}, Different={money != differentMoney}");

        OrderStatus status = OrderStatus.Pending;
        Console.WriteLine($"Enumeration: {status.Id} / {status.Name}");
        Console.WriteLine($"Enumeration all: {string.Join(", ", Enumeration.GetAll<OrderStatus>())}");
        Console.WriteLine($"CompareTo: {status.CompareTo(OrderStatus.Paid)}");

        DemoAggregateRoot();
        DemoAuditableEntity();
    }

    private static void DemoAggregateRoot()
    {
        OrderAggregate aggregate = new(new CustomerId(Guid.NewGuid()));
        aggregate.Raise(new OrderCreatedEvent());

        Console.WriteLine($"Aggregate domain events: {aggregate.DomainEvents.Count}");
        IReadOnlyCollection<IDomainEvent> events = aggregate.TakeEvents();
        Console.WriteLine($"Dequeued events: {events.Count}; pending={aggregate.DomainEvents.Count}");
        aggregate.ClearDomainEvents();
    }

    private static void DemoAuditableEntity()
    {
        AuditableCustomer entity = new(new CustomerId(Guid.NewGuid()));

        entity.DisplayAudit();
    }

    private static void DemoSpecifications()
    {
        Specification<Customer> namedAlice = new CustomerNameSpecification("Alice");
        Specification<Customer> namedBob = new CustomerNameSpecification("Bob");

        Specification<Customer> and = namedAlice.And(new CustomerHasMinimumNameLengthSpecification(3));
        Specification<Customer> or = namedAlice.Or(namedBob);
        Specification<Customer> not = namedAlice.Not();

        Customer alice = new(new CustomerId(Guid.NewGuid()), "Alice");
        Customer bob = new(new CustomerId(Guid.NewGuid()), "Bob");

        Console.WriteLine($"And(Alice): {and.Criteria.Compile()(alice)}");
        Console.WriteLine($"Or(Bob): {or.Criteria.Compile()(bob)}");
        Console.WriteLine($"Not(Alice): {not.Criteria.Compile()(alice)}");

        Specification<Customer> operatorAnd = namedAlice & new CustomerHasMinimumNameLengthSpecification(5);
        Specification<Customer> operatorOr = namedAlice | namedBob;
        Specification<Customer> operatorNot = !namedAlice;

        Console.WriteLine($"&={operatorAnd.Criteria.Compile()(alice)}");
        Console.WriteLine($"|={operatorOr.Criteria.Compile()(bob)}");
        Console.WriteLine($"!={operatorNot.Criteria.Compile()(bob)}");

        Console.WriteLine($"And type={and.GetType().Name}, Or type={or.GetType().Name}, Not type={not.GetType().Name}");
    }

    private static void DemoDomainEvents()
    {
        DomainEventCollection collection = new();
        OrderCreatedEvent first = new();
        OrderCreatedEvent second = new();

        collection.Add(first);
        collection.Add(second);
        Console.WriteLine($"Collection count={collection.Count}, empty={collection.IsEmpty}");
        Console.WriteLine($"Items={collection.Items.Count}");
        Console.WriteLine($"Enumeration: {string.Join(", ", collection.Select(e => e.GetType().Name))}");

        bool removed = collection.Remove(first);
        Console.WriteLine($"Removed={removed}, count={collection.Count}");

        IReadOnlyCollection<IDomainEvent> snapshot = collection.Dequeue();
        Console.WriteLine($"Dequeued={snapshot.Count}, now empty={collection.IsEmpty}");

        RecordingDispatcher recording = new();
        DomainEventDispatcher dispatcher = new(recording);
        dispatcher.DispatchAsync([first, second]).GetAwaiter().GetResult();
        Console.WriteLine($"Dispatched={recording.Count}");

        RecordingHandler handler = new();
        handler.HandleAsync(first).GetAwaiter().GetResult();
        Console.WriteLine($"Handler count={handler.Count}");

        Expect<ArgumentNullException>(() => new DomainEventDispatcher(null!));
    }

    private static void DemoTime()
    {
        DateTimeOffset initial = new(2026, 1, 10, 12, 0, 0, TimeSpan.Zero);
        FakeClock clock = new(initial);

        Console.WriteLine($"UtcNow={clock.UtcNow:o}");
        Console.WriteLine($"Today={clock.Today()} CurrentTime={clock.CurrentTime()}");
        Console.WriteLine($"Weekend={clock.IsWeekend()} Weekday={clock.IsWeekday()}");
        Console.WriteLine($"IsToday={clock.IsToday(clock.UtcNow)} IsFuture={clock.IsFuture(clock.UtcNow.AddDays(1))} IsPast={clock.IsPast(clock.UtcNow.AddDays(-1))}");

        clock.Advance(TimeSpan.FromDays(1));
        clock.AdvanceDays(1);
        clock.AdvanceHours(2);
        clock.AdvanceMinutes(30);
        clock.AdvanceSeconds(15);
        Console.WriteLine($"After advances={clock.UtcNow:o}");

        clock.Rewind(TimeSpan.FromHours(1));
        clock.RewindDays(1);
        clock.RewindHours(1);
        clock.RewindMinutes(30);
        clock.RewindSeconds(15);
        clock.Set(initial.AddHours(3));
        Console.WriteLine($"After rewinds/set={clock.UtcNow:o}");

        DateTimeOffset date = clock.UtcNow;
        Console.WriteLine($"DateOnly={date.DateTime.ToDateOnly()}, TimeOnly={date.DateTime.ToTimeOnly()}");
        SystemClock system = new();
        Console.WriteLine($"SystemClock={system.UtcNow:o}");
    }

    private static void DemoGlobalization()
    {
        Console.WriteLine($"Invariant={SupportedCulture.Invariant.Name}");
        Console.WriteLine($"Spain={SupportedCulture.SpanishSpain.DisplayName}");
        Console.WriteLine($"Mexico={SupportedCulture.SpanishMexico.NativeName}");
        Console.WriteLine($"USA={SupportedCulture.EnglishUnitedStates.Name}");
        Console.WriteLine($"Supported es-ES={SupportedCulture.IsSupported("es-ES")}");
        Console.WriteLine($"FromName es-MX={SupportedCulture.FromName("es-MX")}");
        Console.WriteLine($"All={string.Join(", ", SupportedCulture.All)}");

        LocalizedString localized = new("customer.name", "Nombre", false);
        Console.WriteLine($"LocalizedString={localized.Key} -> {localized.Value}; missing={localized.ResourceNotFound}");

        DemoLocalizationProvider provider = new();
        ICurrentCultureProvider cultureProvider = new DemoCurrentCultureProvider();
        Console.WriteLine($"CurrentCulture={cultureProvider.CurrentCulture.Name}");
        Console.WriteLine($"CurrentUiCulture={cultureProvider.CurrentUiCulture.Name}");

        ITextLocalizer localizer = provider.Default;
        Console.WriteLine($"Get={localizer.Get("hello")}");
        Console.WriteLine($"Formatted={localizer.Get("hello.name", "Alice")}");
        Console.WriteLine($"TryGet={localizer.TryGet("hello", out string value)} -> {value}");
        Console.WriteLine($"Scoped={provider.GetLocalizer("orders").Get("created")}");
    }

    private static void DemoAbstractionsAndCapabilities()
    {
        DemoSoftDelete softDelete = new();
        Console.WriteLine($"ISoftDelete: deleted={softDelete.IsDeleted}, deletedOn={softDelete.DeletedOn}");

        Customer customer = new(new CustomerId(Guid.NewGuid()), "Alice");
        IEntity entity = customer;
        IEntity<CustomerId> typedEntity = customer;
        Console.WriteLine($"IEntity.Id={entity.Id}");
        Console.WriteLine($"IEntity<T>.Id={typedEntity.Id}");

        IAggregateRoot aggregate = new OrderAggregate(new CustomerId(Guid.NewGuid()));
        Console.WriteLine($"IAggregateRoot implemented={aggregate is IAggregateRoot}");

        IHasDomainEvents events = (IHasDomainEvents)aggregate;
        Console.WriteLine($"IHasDomainEvents count={events.DomainEvents.Count}");

        IClock clock = new FakeClock(DateTimeOffset.UtcNow);
        Console.WriteLine($"IClock.UtcNow={clock.UtcNow:o}");

        IEntityId entityId = new CustomerId(Guid.NewGuid());
        Console.WriteLine($"IEntityId.Value={entityId.Value}");
    }

    private static void ShowPublicApiInventory()
    {
        Assembly assembly = typeof(Enumeration).Assembly;

        IEnumerable<Type> publicTypes = assembly
            .GetExportedTypes()
            .Where(type => !type.IsNested)
            .OrderBy(type => type.Namespace)
            .ThenBy(type => type.Name);

        int typeCount = 0;
        int memberCount = 0;

        foreach (Type type in publicTypes)
        {
            typeCount++;
            Console.WriteLine();
            Console.WriteLine($"{type.FullName}");

            IEnumerable<MemberInfo> members = type
                .GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(member => member.MemberType is MemberTypes.Method or MemberTypes.Property or MemberTypes.Field or MemberTypes.Constructor)
                .OrderBy(member => member.MemberType)
                .ThenBy(member => member.Name);

            foreach (MemberInfo member in members)
            {
                memberCount++;
                Console.WriteLine($"  - {member.MemberType}: {member.Name}");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Exported public types: {typeCount}");
        Console.WriteLine($"Declared public members: {memberCount}");
    }

    private static void Expect<TException>(Action action)
        where TException : Exception
    {
        try
        {
            action();
            Console.WriteLine($"Expected {typeof(TException).Name}, but no exception was thrown.");
        }
        catch (TException)
        {
            Console.WriteLine($"Expected {typeof(TException).Name}: OK");
        }
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();
    }

    private sealed class AttributeDemo
    {
        [ValueObjectMember]
        [EqualityOrder(1)]
        public string Name { get; set; } = string.Empty;

        [IgnoreEquality]
        public string Ignored { get; set; } = string.Empty;
    }

    private sealed class CustomerId(Guid value) : GuidEntityId(value)
    {
    }

    private sealed class IntId(int value) : IntEntityId(value)
    {
    }

    private sealed class LongId(long value) : LongEntityId(value)
    {
    }

    private sealed class GuidId(Guid value) : GuidEntityId(value)
    {
    }

    private sealed class StringId(string value) : StringEntityId(value)
    {
    }

    private sealed class IntIdGenerator : IIdGenerator<IntId>
    {
        public IntId New() => new(Random.Shared.Next(1, int.MaxValue));
    }

    private sealed class Customer(CustomerId id, string name) : Entity<CustomerId>(id)
    {
        public string Name { get; } = name;
    }

    private sealed class Money(decimal amount, string currency) : ValueObject
    {
        public decimal Amount { get; } = amount;
        public string Currency { get; } = currency;

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }

    private sealed class OrderStatus(int id, string name) : Enumeration(id, name)
    {
        public static readonly OrderStatus Pending = new(1, "Pending");
        public static readonly OrderStatus Paid = new(2, "Paid");
    }

    private sealed class OrderCreatedEvent : DomainEvent
    {
    }

    private sealed class OrderAggregate(CustomerId id) : AggregateRoot<CustomerId>(id)
    {
        public void Raise(IDomainEvent domainEvent) => AddDomainEvent(domainEvent);
        public IReadOnlyCollection<IDomainEvent> TakeEvents() => DequeueDomainEvents();
    }

    private sealed class AuditableCustomer(CustomerId id) : AuditableEntity<CustomerId>(id)
    {
        public void DisplayAudit()
        {
            Console.WriteLine($"CreatedOn={CreatedOn:o}");
            Console.WriteLine($"ModifiedOn={(ModifiedOn.HasValue ? ModifiedOn.Value.ToString("o") : "<null>")}");
        }
    }

    private sealed class CustomerNameSpecification(string expected) : Specification<Customer>
    {
        public override Expression<Func<Customer, bool>> Criteria =>
            customer => customer.Name == expected;
    }

    private sealed class CustomerHasMinimumNameLengthSpecification(int minimumLength) : Specification<Customer>
    {
        public override Expression<Func<Customer, bool>> Criteria =>
            customer => customer.Name.Length >= minimumLength;
    }

    private sealed class RecordingDispatcher : IDomainEventDispatcher
    {
        public int Count { get; private set; }

        public Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            Count++;
            return Task.CompletedTask;
        }
    }

    private sealed class RecordingHandler : IDomainEventHandler<OrderCreatedEvent>
    {
        public int Count { get; private set; }

        public Task HandleAsync(OrderCreatedEvent domainEvent, CancellationToken cancellationToken = default)
        {
            Count++;
            return Task.CompletedTask;
        }
    }

    private sealed class DemoSoftDelete : ISoftDelete
    {
        public bool IsDeleted => false;
        public DateTimeOffset? DeletedOn => null;
    }

    private sealed class DemoTextLocalizer : ITextLocalizer
    {
        private readonly Dictionary<string, string> _values = new()
        {
            ["hello"] = "Hello",
            ["hello.name"] = "Hello, {0}",
            ["created"] = "Created"
        };

        public string Get(string key) => _values.TryGetValue(key, out string? value) ? value : key;

        public string Get(string key, params object?[] arguments) =>
            string.Format(CultureInfo.InvariantCulture, Get(key), arguments);

        public bool TryGet(string key, out string value) => _values.TryGetValue(key, out value!);
    }

    private sealed class DemoCurrentCultureProvider : ICurrentCultureProvider
    {
        public CultureInfo CurrentCulture => CultureInfo.CurrentCulture;
        public CultureInfo CurrentUiCulture => CultureInfo.CurrentUICulture;
    }

    private sealed class DemoLocalizationProvider : ILocalizationProvider
    {
        private readonly ITextLocalizer _default = new DemoTextLocalizer();

        public ITextLocalizer Default => _default;

        public ITextLocalizer GetLocalizer(string scope) => _default;
    }
}
