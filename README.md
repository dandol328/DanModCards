# DanModCards
Dan &amp; Friends stupid pack of Rounds Cards.

## Building

The project requires the [ROUNDS](https://store.steampowered.com/app/1557740/ROUNDS/) game to be installed, as it references DLLs from the game directory and its BepInEx installation.

### Prerequisites (all platforms)

- [.NET SDK 6.0 or later](https://dotnet.microsoft.com/download)
- ROUNDS installed via Steam
- [BepInEx 5.x](https://github.com/BepInEx/BepInEx/releases) installed in the ROUNDS game directory
- [Unbound Lib](https://github.com/Rounds-Modding/UnboundLib) - Unbound lib is what the cards are built off of

### Windows

1. Install the [.NET SDK](https://dotnet.microsoft.com/download).
2. Set the `ROUNDS_INSTALL_PATH` environment variable to your ROUNDS install directory (or edit `Directory.Build.props` directly):
   ```powershell
   $env:ROUNDS_INSTALL_PATH = "C:\Program Files (x86)\Steam\steamapps\common\Rounds"
   ```
3. Clone the repository and build:
   ```powershell
   git clone https://github.com/dandol328/DanModCards.git
   cd DanModCards
   dotnet build
   ```
4. The compiled `DanModCards.dll` will be in `bin\Debug\netstandard2.1\`.
5. Copy `DanModCards.dll` to `<ROUNDS_INSTALL_PATH>\BepInEx\plugins\DanModCards\`.

### Ubuntu / Debian

1. Install the .NET SDK:
   ```bash
   sudo apt-get update
   sudo apt-get install -y dotnet-sdk-8.0
   ```
2. Set the `ROUNDS_INSTALL_PATH` environment variable to your ROUNDS install directory.  
   The default Steam library path is `~/.steam/steam/steamapps/common/Rounds`:
   ```bash
   export ROUNDS_INSTALL_PATH="$HOME/.steam/steam/steamapps/common/Rounds"
   ```
   Add the line above to your `~/.bashrc` (or `~/.profile`) to make it permanent.
3. Clone the repository and build:
   ```bash
   git clone https://github.com/dandol328/DanModCards.git
   cd DanModCards
   dotnet build
   ```
4. The compiled `DanModCards.dll` will be in `bin/Debug/netstandard2.1/`.
5. Copy `DanModCards.dll` to `$ROUNDS_INSTALL_PATH/BepInEx/plugins/DanModCards/`.

### Bazzite OS (via Distrobox)

Bazzite is an immutable OS and does not support installing packages directly on the host. Use [Distrobox](https://distrobox.it/) to create an Ubuntu container for building.

1. Open a terminal and create an Ubuntu Distrobox container:
   ```bash
   distrobox create --name ubuntu-dev --image ubuntu:24.04
   distrobox enter ubuntu-dev
   ```
2. Inside the container, install the .NET SDK:
   ```bash
   sudo apt-get update
   sudo apt-get install -y dotnet-sdk-8.0
   ```
3. Your home directory and Steam library are automatically shared with the container. Set `ROUNDS_INSTALL_PATH`:
   ```bash
   export ROUNDS_INSTALL_PATH="$HOME/.steam/steam/steamapps/common/Rounds"
   ```
   Add this line to `~/.bashrc` inside the container to make it permanent.
4. Clone the repository and build:
   ```bash
   git clone https://github.com/dandol328/DanModCards.git
   cd DanModCards
   dotnet build
   ```
5. The compiled `DanModCards.dll` will be in `bin/Debug/netstandard2.1/`.
6. Copy `DanModCards.dll` to `$ROUNDS_INSTALL_PATH/BepInEx/plugins/DanModCards/`.  
   Since your home directory is shared, you can do this from inside or outside the container.
