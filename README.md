# Unity Units

[![Test package](https://github.com/Max-Brandt/UnityUnits/actions/workflows/test-package.yml/badge.svg)](https://github.com/Max-Brandt/UnityUnits/actions/workflows/test-package.yml)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

Strongly typed physical units and quantities for Unity — define a value once with its unit (`new Length(5, Length.Units.Meter)`), convert it to any other unit of the same quantity, and combine quantities via normal operators (`Length / Duration = Velocity`, `Mass * Acceleration = Force`, ...).

## Requirements

- Unity `2022.3.36f1` or newer 

## Installation

### Via Unity Package Manager (Git URL)

1. Open **Window → Package Manager**
2. Click **+ → Add package from git URL...**
3. Enter:
   ```
   https://github.com/Max-Brandt/UnityUnits.git
   ```

### Via `manifest.json`

Add the dependency directly to `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.unity.units": "https://github.com/Max-Brandt/UnityUnits.git"
  }
}
```

To pin a specific version, append a tag or commit hash: `...UnityUnits.git#<tag-or-commit>`.

## Usage

```csharp
using UnityEngine;

var distance = new Length(5, Length.Units.Kilometer);
var timeTaken = new Duration(2, Duration.Units.Hour);

// Convert to any unit of the same quantity
float meters = distance.To(Length.Units.Meter); // 5000

// Combine quantities via operators
Velocity speed = distance / timeTaken;
Debug.Log(speed.To(Velocity.Units.KilometerPerHour)); // 2.5

var mass = new Mass(10, Mass.Units.Kilogramm);
var acceleration = new Acceleration(9.81f, Acceleration.Units.MeterPerSquareSecound);
Force weight = mass * acceleration;
```

## Available quantities

| Category   | Quantities                                                    |
|------------|-----------------------------------------------------------------|
| SI         | Length, Mass, Duration, Temperature                              |
| Spatial    | Area, Angle, Volume                                              |
| Kinematics | Velocity, Acceleration, Jerk                                     |
| Dynamics   | Force, Torque, Impulse                                           |
| Fluid      | Density                                                          |
| Thermal    | HeatFlux, HeatTransferCoefficient, ThermalConductivity, ThermalResistance |

Every quantity is a `[Serializable] struct` implementing `IPhysicalValue<TUnit>`, so values show up and can be edited directly in the Inspector.

## Testing

Unit tests live under `Tests/Runtime` and run via Unity's Test Runner (**Window → General → Test Runner**, EditMode tab).

### Continuous Integration

Every push and pull request is built and tested automatically via [GitHub Actions](.github/workflows/test-package.yml) using [`willykc/unity-package-tester`](https://github.com/willykc/unity-package-tester) (which wraps [game-ci/unity-test-runner](https://github.com/game-ci/unity-test-runner)). It imports the package into a clean Unity project and runs the EditMode test suite — see the badge above for the current status, or check the [Actions tab](https://github.com/Max-Brandt/UnityUnits/actions) for full run logs.

## License

[MIT](LICENSE) © Max Brandt
