<a id="readme-top"></a>
﻿<!-- PROJECT LOGO -->
<div align="center">
  <a href="https://github.com/Hawk200014/FallenTally">
    <img src="src\Resources\Icons\AppIcon.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Fallen Tally</h3>

  <p align="center">
    A simple death counter for games to display inside obs.
    <br />
    <a href="https://github.com/Hawk200014/FallenTally"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/Hawk200014/FallenTally/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    &middot;
    <a href="https://github.com/Hawk200014/FallenTally/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

Fallen Tally is a simple application designed to keep track of failed attemps in games.
It also allows to display the stats as a obs overlay so that the viewer can see exactly how boosted you are in a game :-)
The taken stats can be exported into different formats so that you can make videos or highlights of your stream / recording.

### Features

- keep track of the stats ingame
  - add games
  - add locations inside games (intended for bosses)
  - keep track of attemps to defeat certain locations
- add markers for funny events in your recording / streaming session
- export data for later use in cutting or in compilations
  - export deaths with the date and the stream / recording timestamp
  - export marker with the date and the stream / recording timestamp
- configure hotkeys and the overlay
  - configure the html document for the obs overlay
  - set hotkeys to quickly interact with the programm when you are inside the game

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

* .NET 8.0 SDK
  ```sh
  dotnet --version
  ```

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/Hawk200014/FallenTally.git
   ```
2. Navigate to the project directory
   ```sh
   cd DeathCounter
   ```
3. Restore dependencies
   ```sh
   dotnet restore
   ```
4. Build the project
   ```sh
   dotnet build
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- USAGE EXAMPLES -->
## Usage

To run the application, use the following command:
```sh
dotnet run --project src/FallenTally
```

_For more examples, please refer to the [Documentation](https://example.com)_

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ROADMAP -->
## Roadmap

- [X] Export Marker
- [X] More Marker
- [ ] Update UI
- [ ] Refactor and Optimize Code
- [ ] Update to own API infastructure

See the [open issues](https://github.com/Hawk200014/FallenTally/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>
