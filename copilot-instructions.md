# Copilot Instructions for Project Euphoria

This document provides guidelines and instructions for working on Project Euphoria, a MonoGame-based game project.

## Building the Project

### Prerequisites
- .NET 9.0 SDK or later
- dotnet CLI tools

### Command Line Build

To build the project from the command line:

```bash
# Navigate to the repository root
cd /path/to/ProjectEuphoria

# Restore dependencies and build the solution
dotnet build Euphoria.sln

# Build in Release configuration
dotnet build Euphoria.sln --configuration Release

# Run the application
dotnet run --project src/Platforms/DesktopGL/Euphoria.DesktopGL.csproj
```

### Clean Build

To perform a clean build:

```bash
# Clean the solution
dotnet clean Euphoria.sln

# Rebuild from scratch
dotnet build Euphoria.sln
```

## Code Formatting Rules

### General C# Conventions

Follow standard C# coding conventions as outlined in the [Microsoft C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).

### Specific Rules

1. **Namespaces**: Use file-scoped namespace declarations
   ```csharp
   namespace Core;
   
   public class MyClass
   {
       // Class implementation
   }
   ```

2. **Naming Conventions**:
   - Classes, methods, and public properties: PascalCase
   - Private fields: camelCase with underscore prefix (e.g., `_graphics`)
   - Local variables and parameters: camelCase

3. **Formatting**:
   - Use 4 spaces for indentation (no tabs)
   - Place opening braces on a new line
   - Use `var` for local variables when the type is obvious

4. **Using Directives**:
   - Place using directives at the top of the file
   - Order: System namespaces first, then third-party, then project namespaces
   - Remove unused using directives

### Code Formatting Tool

The project supports `dotnet format` for automatic code formatting:

```bash
# Format all code in the solution
dotnet format Euphoria.sln

# Check for formatting issues without applying changes
dotnet format Euphoria.sln --verify-no-changes
```

## Important Restrictions

### System.Numerics Namespace

**DO NOT use the `System.Numerics` namespace in this project.**

This project uses MonoGame's built-in types for vectors, matrices, and other mathematical operations. Use the following MonoGame types instead:

- Use `Microsoft.Xna.Framework.Vector2` instead of `System.Numerics.Vector2`
- Use `Microsoft.Xna.Framework.Vector3` instead of `System.Numerics.Vector3`
- Use `Microsoft.Xna.Framework.Vector4` instead of `System.Numerics.Vector4`
- Use `Microsoft.Xna.Framework.Matrix` instead of `System.Numerics.Matrix4x4`
- Use `Microsoft.Xna.Framework.Quaternion` instead of `System.Numerics.Quaternion`

Example:
```csharp
// ❌ DO NOT DO THIS
using System.Numerics;
Vector2 position = new Vector2(10, 20);

// ✅ DO THIS INSTEAD
using Microsoft.Xna.Framework;
Vector2 position = new Vector2(10, 20);
```

## Project Structure

```
ProjectEuphoria/
├── Euphoria.sln              # Solution file
├── src/
│   ├── Code/                 # Shared game code
│   │   └── EuphoriaGame.cs   # Main game class
│   ├── Content/              # Game assets (textures, sounds, etc.)
│   └── Platforms/
│       └── DesktopGL/        # Desktop platform implementation
│           ├── Euphoria.DesktopGL.csproj
│           └── Program.cs    # Entry point
├── .config/
│   └── dotnet-tools.json     # MonoGame content pipeline tools
└── README.md
```

## Development Workflow

1. Make code changes in the `src/Code/` directory for cross-platform code
2. Use the MonoGame Content Pipeline (`mgcb`) for managing game assets
3. Build and test frequently using `dotnet build` and `dotnet run`
4. Format code before committing: `dotnet format`
5. Ensure System.Numerics is not used anywhere in the codebase

## Additional Resources

- [MonoGame Documentation](https://docs.monogame.net/)
- [MonoGame API Reference](https://docs.monogame.net/api/index.html)
- [C# Programming Guide](https://learn.microsoft.com/en-us/dotnet/csharp/)
