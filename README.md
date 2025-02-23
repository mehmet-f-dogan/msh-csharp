# A Custom Shell using C#

This repository implements a custom shell in C#. The shell includes essential functionalities like command execution, navigation, quoting, and redirection.

## Features

### Core Functionalities

- **Print a prompt**: Displays a customizable shell prompt to the user.
- **Handle invalid commands**: Gracefully handles unrecognized commands with appropriate error messages.
- **REPL (Read-Eval-Print Loop)**: Implements an interactive loop for continuous command execution.
- **The exit builtin**: Allows the user to exit the shell.

### Built-in Commands

#### `echo`

- Repeats the arguments provided.
- Supports quoting for spaces and special characters.

#### `type`

- **Builtins**: Identifies and displays shell built-in commands.
- **Executable files**: Checks and displays information about executable files in the user's PATH.

### Running External Programs

- Executes programs and handles errors for missing or inaccessible executables.

### Navigation

#### `pwd`

- Prints the current working directory.

#### `cd`

- **Absolute paths**: Navigates to directories using absolute paths.
- **Relative paths**: Navigates using paths relative to the current directory.
- **Home directory**: Navigates to the user's home directory.

### Quoting

- **Single quotes**: Preserves the literal value of all characters within the quotes.
- **Double quotes**: Allows interpolation of variables and special characters.
- **Backslash outside quotes**: Escapes the next character.
- **Backslash within quotes**:
  - **Within single quotes**: Treated literally.
  - **Within double quotes**: Escapes special characters like `"` and `$`.

#### Executing a Quoted Executable

- Handles execution of commands where the executable name is quoted.

### Redirection

- **Redirect stdout**: Supports `>` to redirect standard output to a file.
- **Redirect stderr**: Supports `2>` to redirect standard error to a file.
- **Append stdout**: Supports `>>` to append standard output to a file.
- **Append stderr**: Supports `2>>` to append standard error to a file.

## Usage

Once the shell is running, you can use the following commands:

- `pwd`: Prints the current directory.
- `cd [directory]`: Navigates to the specified directory.
- `echo [arguments]`: Prints the arguments.
- `type [command]`: Displays information about the command.
- Redirect output or error streams with `>`, `2>`, `>>`, `2>>`.

## Examples

### Navigation

```bash
> pwd
/home/user
> cd /tmp
> pwd
/tmp
```

### Quoting

```bash
> echo "Hello, World!"
Hello, World!
> echo 'Single quoted: \n'
Single quoted: \n
> echo "Backslash: \n"
Backslash:
```

### Redirection

```bash
> echo "Hello" > file.txt
> cat file.txt
Hello
> echo "Append this" >> file.txt
> cat file.txt
Hello
Append this
```
