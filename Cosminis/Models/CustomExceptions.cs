namespace CustomExceptions;


public class ResourceNotFound : Exception
{
    public ResourceNotFound() { }
    public ResourceNotFound(string message) : base(message) { }
    public ResourceNotFound(string message, System.Exception inner) : base(message, inner) { }
    protected ResourceNotFound(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public class UsernameNotAvailable : Exception
{
    public UsernameNotAvailable() { }
    public UsernameNotAvailable(string message) : base(message) { }
    public UsernameNotAvailable(string message, System.Exception inner) : base(message, inner) { }
    protected UsernameNotAvailable(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
public class InvalidCredentials : Exception
{
    public InvalidCredentials() { }
    public InvalidCredentials(string message) : base(message) { }
    public InvalidCredentials(string message, System.Exception inner) : base(message, inner) { }
    protected InvalidCredentials(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public class UnsuccessfulRegistration : Exception
{
    public UnsuccessfulRegistration() { }
    public UnsuccessfulRegistration(string message) : base(message) { }
    public UnsuccessfulRegistration(string message, System.Exception inner) : base(message, inner) { }
    protected UnsuccessfulRegistration(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public class SpeciesNotFound : Exception
{
    public SpeciesNotFound() { }
    public SpeciesNotFound(string message) : base(message) { }
    public SpeciesNotFound(string message, System.Exception inner) : base(message, inner) { }
    protected SpeciesNotFound(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public class MoodNotFound : Exception
{
    public MoodNotFound() { }
    public MoodNotFound(string message) : base(message) { }
    public MoodNotFound(string message, System.Exception inner) : base(message, inner) { }
    protected MoodNotFound(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public class FoodElementNotFound : Exception
{
    public FoodElementNotFound() { }
    public FoodElementNotFound(string message) : base(message) { }
    public FoodElementNotFound(string message, System.Exception inner) : base(message, inner) { }
    protected FoodElementNotFound(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public class PostsNotFound : Exception
{
    public PostsNotFound() { }
    public PostsNotFound(string message) : base(message) { }
    public PostsNotFound(string message, System.Exception inner) : base(message, inner) { }
    protected PostsNotFound(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}