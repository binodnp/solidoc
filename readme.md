# Moved to Node

This project is now rewritten in JavaScript.

https://github.com/binodnp/solidoc-js



## Solidoc--Documentation Generator for Solidity

This command-line utility creates markdown-based documentation for your Solidity projects.

## Getting Started

- Clone the project. `git clone https://github.com/binodnp/solidoc`
- Compile the project. `dotnet build`
- Create a bash script to automate documentation generation as a part of your build process.

***nix**

```sh
dotnet solidoc.dll <path to project> <path to generate documentation to>
```

**Windows**

```sh
solidoc.exe <path to project> <path to generate documentation to>
```


## Examples:

- [OpenZeppelin Solidity Documentation](https://github.com/binodnp/openzeppelin-solidity/blob/master/docs/ERC721.md)
- [Open Source Vesting Schedule by Binod](https://github.com/binodnp/vesting-schedule/blob/master/docs/VestingSchedule.md)
- [Virtual Rehab Token](https://github.com/ViRehab/VirtualRehabToken/blob/master/docs/VRHToken.md)
- [Virtual Rehab Private Sale](https://github.com/ViRehab/VirtualRehabPrivateSale/blob/master/docs/PrivateSale.md)
