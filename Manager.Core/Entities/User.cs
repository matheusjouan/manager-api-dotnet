using Manager.Core.Excpetions;
using Manager.Core.Validators;

namespace Manager.Core.Entities;

public class User : EntityBase
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    // Usando p/ EF
    protected User() { }

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        _errors = new List<string>();
    }

    public void ChangeName(string name)
    {
        Name = name;
        Validate();
    }

    public void ChangeEmail(string email)
    {
        Email = email;
        Validate();
    }

    public void ChangePassword(string password)
    {
        Password = password;
        Validate();
    }


    public override bool Validate()
    {
        var validator = new UserValidator();
        // Irá verificar se a entidade é válida
        var validation = validator.Validate(this);

        if(!validation.IsValid)
        {
            foreach (var error in validation.Errors)
                _errors.Add(error.ErrorMessage);

            throw new DomainExcpetion("Some fields are invalid", _errors);
        }

        return true;
    }
}

