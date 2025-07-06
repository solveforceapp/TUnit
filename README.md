![](assets/banner.png)

# 🚀 The Modern Testing Framework for .NET

**TUnit** is a next-generation testing framework for C# that outpaces traditional frameworks with **source-generated tests**, **parallel execution by default**, and **Native AOT support**. Built on the modern Microsoft.Testing.Platform, TUnit delivers faster test runs, better developer experience, and unmatched flexibility.

<div align="center">

[![thomhurst%2FTUnit | Trendshift](https://trendshift.io/api/badge/repositories/11781)](https://trendshift.io/repositories/11781)


[![Codacy Badge](https://api.codacy.com/project/badge/Grade/a8231644d844435eb9fd15110ea771d8)](https://app.codacy.com/gh/thomhurst/TUnit?utm_source=github.com&utm_medium=referral&utm_content=thomhurst/TUnit&utm_campaign=Badge_Grade)![GitHub Repo stars](https://img.shields.io/github/stars/thomhurst/TUnit) ![GitHub Issues or Pull Requests](https://img.shields.io/github/issues-closed-raw/thomhurst/TUnit)
 [![GitHub Sponsors](https://img.shields.io/github/sponsors/thomhurst)](https://github.com/sponsors/thomhurst) [![nuget](https://img.shields.io/nuget/v/TUnit.svg)](https://www.nuget.org/packages/TUnit/) [![NuGet Downloads](https://img.shields.io/nuget/dt/TUnit)](https://www.nuget.org/packages/TUnit/) ![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/thomhurst/TUnit/dotnet.yml) ![GitHub last commit (branch)](https://img.shields.io/github/last-commit/thomhurst/TUnit/main) ![License](https://img.shields.io/github/license/thomhurst/TUnit)

</div>

## ⚡ Why Choose TUnit?

| Feature | Traditional Frameworks | **TUnit** |
|---------|----------------------|-----------|
| Test Discovery | ❌ Runtime reflection | ✅ **Compile-time generation** |
| Execution Speed | ❌ Sequential by default | ✅ **Parallel by default** |
| Modern .NET | ⚠️ Limited AOT support | ✅ **Full Native AOT & trimming** |
| Test Dependencies | ❌ Not supported | ✅ **`[DependsOn]` chains** |
| Resource Management | ❌ Manual lifecycle | ✅ **Intelligent cleanup** |

⚡ **Parallel by Default** - Tests run concurrently with intelligent dependency management

🎯 **Compile-Time Discovery** - Know your test structure before runtime

🔧 **Modern .NET Ready** - Native AOT, trimming, and latest .NET features

🎭 **Extensible** - Customize data sources, attributes, and test behavior

---

<div align="center">

## 📚 **[Complete Documentation & Learning Center](https://tunit.dev)**

**🚀 New to TUnit?** Start with our **[Getting Started Guide](https://tunit.dev/docs/getting-started/installation)**

**🔄 Migrating?** See our **[Migration Guides](https://tunit.dev/docs/migration/xunit)**

**🎯 Advanced Features?** Explore **[Data-Driven Testing](https://tunit.dev/docs/test-authoring/arguments)**, **[Test Dependencies](https://tunit.dev/docs/test-authoring/depends-on)**, and **[Parallelism Control](https://tunit.dev/docs/parallelism/not-in-parallel)**

</div>

---

## 🏁 Quick Start

### Using the Project Template (Recommended)
```bash
dotnet new install TUnit.Templates
dotnet new TUnit -n "MyTestProject"
```

### Manual Installation
```bash
dotnet add package TUnit --prerelease
```

📖 **[📚 Complete Documentation & Guides](https://tunit.dev)** - Everything you need to master TUnit

## ✨ Key Features

<table>
<tr>
<td width="50%">

**🚀 Performance & Modern Platform**
- 🔥 Source-generated tests (no reflection)
- ⚡ Parallel execution by default
- 🚀 Native AOT & trimming support
- 📈 Optimized for performance

</td>
<td width="50%">

**🎯 Advanced Test Control**
- 🔗 Test dependencies with `[DependsOn]`
- 🎛️ Parallel limits & custom scheduling
- 🛡️ Built-in analyzers & compile-time checks
- 🎭 Custom attributes & extensible conditions

</td>
</tr>
<tr>
<td>

**📊 Rich Data & Assertions**
- 📋 Multiple data sources (`[Arguments]`, `[Matrix]`, `[ClassData]`)
- ✅ Fluent async assertions
- 🔄 Smart retry logic & conditional execution
- 📝 Rich test metadata & context

</td>
<td>

**🔧 Developer Experience**
- 💉 Full dependency injection support
- 🪝 Comprehensive lifecycle hooks
- 🎯 IDE integration (VS, Rider, VS Code)
- 📚 Extensive documentation & examples

</td>
</tr>
</table>

## 📝 Simple Test Example

```csharp
[Test]
public async Task User_Creation_Should_Set_Timestamp()
{
    // Arrange
    var userService = new UserService();

    // Act
    var user = await userService.CreateUserAsync("john.doe@example.com");

    // Assert - TUnit's fluent assertions
    await Assert.That(user.CreatedAt)
        .IsEqualTo(DateTime.Now)
        .Within(TimeSpan.FromMinutes(1));

    await Assert.That(user.Email)
        .IsEqualTo("john.doe@example.com");
}
```

## 🎯 Data-Driven Testing

```csharp
[Test]
[Arguments("user1@test.com", "ValidPassword123")]
[Arguments("user2@test.com", "AnotherPassword456")]
[Arguments("admin@test.com", "AdminPass789")]
public async Task User_Login_Should_Succeed(string email, string password)
{
    var result = await authService.LoginAsync(email, password);
    await Assert.That(result.IsSuccess).IsTrue();
}

// Matrix testing - tests all combinations
[Test]
[MatrixDataSource]
public async Task Database_Operations_Work(
    [Matrix("Create", "Update", "Delete")] string operation,
    [Matrix("User", "Product", "Order")] string entity)
{
    await Assert.That(await ExecuteOperation(operation, entity))
        .IsTrue();
}
```

## 🔗 Advanced Test Orchestration

```csharp
[Before(Class)]
public static async Task SetupDatabase(ClassHookContext context)
{
    await DatabaseHelper.InitializeAsync();
}

[Test, DisplayName("Register a new account")]
[MethodDataSource(nameof(GetTestUsers))]
public async Task Register_User(string username, string password)
{
    // Test implementation
}

[Test, DependsOn(nameof(Register_User))]
[Retry(3)] // Retry on failure
public async Task Login_With_Registered_User(string username, string password)
{
    // This test runs after Register_User completes
}

[Test]
[ParallelLimit<LoadTestParallelLimit>] // Custom parallel control
[Repeat(100)] // Run 100 times
public async Task Load_Test_Homepage()
{
    // Performance testing
}

// Custom attributes
[Test, WindowsOnly, RetryOnHttpError(5)]
public async Task Windows_Specific_Feature()
{
    // Platform-specific test with custom retry logic
}

public class LoadTestParallelLimit : IParallelLimit
{
    public int Limit => 10; // Limit to 10 concurrent executions
}
```

## 🔧 Smart Test Control

```csharp
// Custom conditional execution
public class WindowsOnlyAttribute : SkipAttribute
{
    public WindowsOnlyAttribute() : base("Windows only test") { }

    public override Task<bool> ShouldSkip(TestContext testContext)
        => Task.FromResult(!OperatingSystem.IsWindows());
}

// Custom retry logic
public class RetryOnHttpErrorAttribute : RetryAttribute
{
    public RetryOnHttpErrorAttribute(int times) : base(times) { }

    public override Task<bool> ShouldRetry(TestInformation testInformation,
        Exception exception, int currentRetryCount)
        => Task.FromResult(exception is HttpRequestException { StatusCode: HttpStatusCode.ServiceUnavailable });
}
```

## 🎯 Perfect For Every Testing Scenario

<table>
<tr>
<td width="33%">

### 🧪 **Unit Testing**
```csharp
[Test]
[Arguments(1, 2, 3)]
[Arguments(5, 10, 15)]
public async Task Calculate_Sum(int a, int b, int expected)
{
    await Assert.That(Calculator.Add(a, b))
        .IsEqualTo(expected);
}
```
**Fast, isolated, and reliable**

</td>
<td width="33%">

### 🔗 **Integration Testing**
```csharp
[Test, DependsOn(nameof(CreateUser))]
public async Task Login_After_Registration()
{
    // Runs after CreateUser completes
    var result = await authService.Login(user);
    await Assert.That(result.IsSuccess).IsTrue();
}
```
**Stateful workflows made simple**

</td>
<td width="33%">

### ⚡ **Load Testing**
```csharp
[Test]
[ParallelLimit<LoadTestLimit>]
[Repeat(1000)]
public async Task API_Handles_Concurrent_Requests()
{
    await Assert.That(await httpClient.GetAsync("/api/health"))
        .HasStatusCode(HttpStatusCode.OK);
}
```
**Built-in performance testing**

</td>
</tr>
</table>

## 🚀 What Makes TUnit Different?

### **Compile-Time Intelligence**
Tests are discovered at build time, not runtime - enabling faster discovery, better IDE integration, and precise resource lifecycle management.

### **Parallel-First Architecture**
Built for concurrency from day one with `[DependsOn]` for test chains, `[ParallelLimit]` for resource control, and intelligent scheduling.

### **Extensible by Design**
The `DataSourceGenerator<T>` pattern and custom attribute system let you extend TUnit's capabilities without modifying core framework code.

## 🏆 Community & Ecosystem

<div align="center">

**🌟 Join thousands of developers modernizing their testing**

[![Downloads](https://img.shields.io/nuget/dt/TUnit?label=Downloads&color=blue)](https://www.nuget.org/packages/TUnit/)
[![Contributors](https://img.shields.io/github/contributors/thomhurst/TUnit?label=Contributors)](https://github.com/thomhurst/TUnit/graphs/contributors)
[![Discussions](https://img.shields.io/github/discussions/thomhurst/TUnit?label=Discussions)](https://github.com/thomhurst/TUnit/discussions)

</div>

### 🤝 **Active Community**
- 📚 **[Official Documentation](https://tunit.dev)** - Comprehensive guides, tutorials, and API reference
- 💬 **[GitHub Discussions](https://github.com/thomhurst/TUnit/discussions)** - Get help and share ideas
- 🐛 **[Issue Tracking](https://github.com/thomhurst/TUnit/issues)** - Report bugs and request features
- 📢 **[Release Notes](https://github.com/thomhurst/TUnit/releases)** - Stay updated with latest improvements

## 🛠️ IDE Support

TUnit works seamlessly across all major .NET development environments:

### Visual Studio (2022 17.13+)
✅ **Fully supported** - No additional configuration needed for latest versions

⚙️ **Earlier versions**: Enable "Use testing platform server mode" in Tools > Manage Preview Features

### JetBrains Rider
✅ **Fully supported**

⚙️ **Setup**: Enable "Testing Platform support" in Settings > Build, Execution, Deployment > Unit Testing > VSTest

### Visual Studio Code
✅ **Fully supported**

⚙️ **Setup**: Install [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) and enable "Use Testing Platform Protocol"

### Command Line
✅ **Full CLI support** - Works with `dotnet test`, `dotnet run`, and direct executable execution

## 📦 Package Options

| Package | Use Case |
|---------|----------|
| **`TUnit`** | ⭐ **Start here** - Complete testing framework (includes Core + Engine + Assertions) |
| **`TUnit.Core`** | 📚 Test libraries and shared components (no execution engine) |
| **`TUnit.Engine`** | 🚀 Test execution engine and adapter (for test projects) |
| **`TUnit.Assertions`** | ✅ Standalone assertions (works with any test framework) |
| **`TUnit.Playwright`** | 🎭 Playwright integration with automatic lifecycle management |

## 🎯 Migration from Other Frameworks

**Coming from NUnit or xUnit?** TUnit maintains familiar syntax while adding modern capabilities:

```csharp
// Enhanced with TUnit's advanced features
[Test]
[Arguments("value1")]
[Arguments("value2")]
[Retry(3)]
[ParallelLimit<CustomLimit>]
public async Task Modern_TUnit_Test(string value) { }
```

📖 **Need help migrating?** Check our detailed **[Migration Guides](https://tunit.dev/docs/migration/xunit)** with step-by-step instructions for xUnit, NUnit, and MSTest.


## 💡 Current Status

The API is mostly stable, but may have some changes based on feedback or issues before v1.0 release.

---

<div align="center">

## 🚀 Ready to Experience the Future of .NET Testing?

### ⚡ **Start in 30 Seconds**

```bash
# Create a new test project with examples
dotnet new install TUnit.Templates && dotnet new TUnit -n "MyAwesomeTests"

# Or add to existing project
dotnet add package TUnit --prerelease
```

### 🎯 **Why Wait? Join the Movement**

<table>
<tr>
<td align="center" width="25%">

### 📈 **Performance**
**Optimized execution**
**Parallel by default**
**Zero reflection overhead**

</td>
<td align="center" width="25%">

### 🔮 **Future-Ready**
**Native AOT support**
**Latest .NET features**
**Source generation**

</td>
<td align="center" width="25%">

### 🛠️ **Developer Experience**
**Compile-time checks**
**Rich IDE integration**
**Intelligent debugging**

</td>
<td align="center" width="25%">

### 🎭 **Flexibility**
**Test dependencies**
**Custom attributes**
**Extensible architecture**

</td>
</tr>
</table>

---

**📖 Learn More**: [tunit.dev](https://tunit.dev) | **💬 Get Help**: [GitHub Discussions](https://github.com/thomhurst/TUnit/discussions) | **⭐ Show Support**: [Star on GitHub](https://github.com/thomhurst/TUnit)

*TUnit is actively developed and production-ready. Join our growing community of developers who've made the switch!*

</div>

## Performance Benchmark

### Scenario: Building the test project

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sonoma 14.7.6 (23H626) [Darwin 23.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.301
  [Host]   : .NET 9.0.6 (9.0.625.26613), Arm64 RyuJIT AdvSIMD
  .NET 9.0 : .NET 9.0.6 (9.0.625.26613), Arm64 RyuJIT AdvSIMD

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method       | Mean       | Error    | StdDev    | Median     |
|------------- |-----------:|---------:|----------:|-----------:|
| Build_TUnit  | 1,318.1 ms | 78.14 ms | 227.92 ms | 1,254.4 ms |
| Build_NUnit  | 1,047.5 ms | 46.41 ms | 133.91 ms | 1,046.9 ms |
| Build_xUnit  |   799.4 ms | 15.58 ms |  13.81 ms |   797.4 ms |
| Build_MSTest |   852.7 ms | 15.18 ms |  21.78 ms |   852.3 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.301
  [Host]   : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method       | Mean    | Error    | StdDev   |
|------------- |--------:|---------:|---------:|
| Build_TUnit  | 1.904 s | 0.0374 s | 0.0431 s |
| Build_NUnit  | 1.456 s | 0.0153 s | 0.0136 s |
| Build_xUnit  | 1.444 s | 0.0231 s | 0.0216 s |
| Build_MSTest | 1.468 s | 0.0160 s | 0.0149 s |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.3807) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.301
  [Host]   : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method       | Mean    | Error    | StdDev   |
|------------- |--------:|---------:|---------:|
| Build_TUnit  | 1.910 s | 0.0379 s | 0.0567 s |
| Build_NUnit  | 1.499 s | 0.0188 s | 0.0167 s |
| Build_xUnit  | 1.505 s | 0.0192 s | 0.0160 s |
| Build_MSTest | 1.549 s | 0.0295 s | 0.0316 s |


### Scenario: A single test that completes instantly (including spawning a new process and initialising the test framework)

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sonoma 14.7.6 (23H626) [Darwin 23.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.301
  [Host]   : .NET 9.0.6 (9.0.625.26613), Arm64 RyuJIT AdvSIMD
  .NET 9.0 : .NET 9.0.6 (9.0.625.26613), Arm64 RyuJIT AdvSIMD

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method    | Mean      | Error     | StdDev    | Median    |
|---------- |----------:|----------:|----------:|----------:|
| TUnit_AOT |  77.80 ms |  1.461 ms |  3.444 ms |  76.59 ms |
| TUnit     | 512.57 ms | 11.408 ms | 33.098 ms | 510.54 ms |
| NUnit     | 694.85 ms |  6.447 ms |  6.031 ms | 693.75 ms |
| xUnit     | 721.75 ms |  9.809 ms |  8.191 ms | 719.92 ms |
| MSTest    | 622.15 ms |  7.202 ms |  6.014 ms | 623.36 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.301
  [Host]   : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method    | Mean        | Error     | StdDev    |
|---------- |------------:|----------:|----------:|
| TUnit_AOT |    27.73 ms |  0.599 ms |  1.748 ms |
| TUnit     |   829.96 ms | 16.213 ms | 24.267 ms |
| NUnit     | 1,283.00 ms | 16.782 ms | 15.698 ms |
| xUnit     | 1,332.19 ms | 15.601 ms | 14.593 ms |
| MSTest    | 1,124.99 ms | 10.606 ms |  9.402 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.3807) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.301
  [Host]   : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method    | Mean        | Error     | StdDev    |
|---------- |------------:|----------:|----------:|
| TUnit_AOT |    53.63 ms |  1.349 ms |  3.914 ms |
| TUnit     |   890.05 ms | 17.626 ms | 26.917 ms |
| NUnit     | 1,358.32 ms | 26.776 ms | 26.298 ms |
| xUnit     | 1,382.84 ms | 15.172 ms | 14.192 ms |
| MSTest    | 1,199.68 ms | 12.706 ms | 11.886 ms |


### Scenario: A test that takes 50ms to execute, repeated 100 times (including spawning a new process and initialising the test framework)

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sonoma 14.7.6 (23H626) [Darwin 23.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.301
  [Host]   : .NET 9.0.6 (9.0.625.26613), Arm64 RyuJIT AdvSIMD
  .NET 9.0 : .NET 9.0.6 (9.0.625.26613), Arm64 RyuJIT AdvSIMD

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method    | Mean        | Error     | StdDev    |
|---------- |------------:|----------:|----------:|
| TUnit_AOT |    237.1 ms |  14.19 ms |  41.85 ms |
| TUnit     |    658.7 ms |  21.02 ms |  61.63 ms |
| NUnit     | 14,031.6 ms | 278.49 ms | 617.12 ms |
| xUnit     | 14,563.3 ms | 289.90 ms | 484.36 ms |
| MSTest    | 14,115.7 ms | 278.43 ms | 556.05 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.301
  [Host]   : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method    | Mean        | Error     | StdDev    | Median      |
|---------- |------------:|----------:|----------:|------------:|
| TUnit_AOT |    75.33 ms |  1.472 ms |  2.158 ms |    74.07 ms |
| TUnit     |   896.22 ms | 16.838 ms | 17.291 ms |   900.50 ms |
| NUnit     | 6,294.49 ms | 26.916 ms | 25.177 ms | 6,281.93 ms |
| xUnit     | 6,472.23 ms | 21.037 ms | 19.678 ms | 6,473.46 ms |
| MSTest    | 6,290.36 ms | 14.621 ms | 13.677 ms | 6,287.34 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.3807) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.301
  [Host]   : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method    | Mean       | Error    | StdDev   |
|---------- |-----------:|---------:|---------:|
| TUnit_AOT |   112.6 ms |  2.18 ms |  3.26 ms |
| TUnit     |   990.6 ms | 19.65 ms | 30.59 ms |
| NUnit     | 7,561.7 ms | 17.59 ms | 15.59 ms |
| xUnit     | 7,625.7 ms | 23.62 ms | 20.94 ms |
| MSTest    | 7,510.4 ms | 26.65 ms | 24.92 ms |



