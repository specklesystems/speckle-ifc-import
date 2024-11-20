namespace Ara3D.StepParser;

public class StepInstance
{
  public readonly StepEntity Entity;
  public readonly uint Id;

  public List<StepValue> AttributeValues => Entity.Attributes.Values;

  public string EntityType => Entity.EntityType.ToString() ?? "";

  public StepInstance(uint id, StepEntity entity)
  {
    Id = id;
    Entity = entity;
  }

  public bool IsEntityType(string str) => EntityType == str;

  public override string ToString() => $"#{Id}={Entity};";

  public int Count => AttributeValues.Count;

  public StepValue this[int i]
#pragma warning disable CA1065
    =>
    i < Count ? AttributeValues[i] : throw new IndexOutOfRangeException();
#pragma warning restore CA1065
}
