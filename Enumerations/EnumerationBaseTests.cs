using TheOmenDen.Shared.Tests.Enumerations.Models;

namespace TheOmenDen.Shared.Tests.Enumerations;
public class EnumerationBaseTests
{
    #region Theory Data
    public static TheoryData<TestEnumeration> TheoryEnumerationData => new()
    {
        TestEnumeration.Option1,
        TestEnumeration.Option2,
        TestEnumeration.Option3
    };

    public static TheoryData<String, TestEnumWithStringKey> StringEnumerationPairTheoryData = new()
    {
        {nameof(TestEnumWithStringKey.Option1), TestEnumWithStringKey.Option1},
        {nameof(TestEnumWithStringKey.Option2), TestEnumWithStringKey.Option2},
        {nameof(TestEnumWithStringKey.Option3), TestEnumWithStringKey.Option3}
    };

    public static TheoryData<TestEnumeration, TestEnumeration, Int32> ComparisonEnumerationTheoryData = new()
    {
        {TestEnumeration.Option2, TestEnumeration.Option1, 1},
        {TestEnumeration.Option2, TestEnumeration.Option2, 0},
        {TestEnumeration.Option2, TestEnumeration.Option3, -1},
    };

    public static TheoryData<TestEnumeration, TestEnumeration, Boolean, Boolean, Boolean> ComparisonOperatorTheoryData =
        new()
        {
            {TestEnumeration.Option2, TestEnumeration.Option1, false, false, true},
            {TestEnumeration.Option2, TestEnumeration.Option2, false, true, false},
            {TestEnumeration.Option2, TestEnumeration.Option3, true, false, false},
        };
    #endregion
    #region ToString Tests
    [Theory]
    [MemberData(nameof(TheoryEnumerationData))]
    public void ToString_ShouldReturnTheEnumerationKeyName(TestEnumeration testEnumeration)
    {
        //ARRANGE && ACT
        var result = testEnumeration.ToString();
        //ASSERT
        result.ShouldBe(testEnumeration.Name);
    }
    #endregion
    #region ReadOnly List Tests
    [Fact]
    public void ReadonlyEnumList_ShouldReturnAllTheDefinedEnumerations()
    {
        //ARRANGE
        var enumerationArrayExpected = new[]
        {
            TestEnumeration.Option1,
            TestEnumeration.Option2,
            TestEnumeration.Option3
        };

        //ACT
        var enumerationListActual = TestEnumeration.ReadOnlyEnumerationList;

        //ASSERT
        enumerationListActual.ShouldBeSubsetOf(enumerationArrayExpected);
    }
    #endregion
    #region Condition Tests (When...Then...)
    [Fact]
    public void Condition_ShouldNotRunDefaultConditionWhenConditionIsMet()
    {
        //ARRANGE
        var enumToTest = TestEnumeration.Option1;
        var actionToTestRunResult = false;
        var defaultActionRunResult = false;

        // ACT
        enumToTest.When(TestEnumeration.Option1).Then(() => actionToTestRunResult = true)
            .DefaultCondition(() => defaultActionRunResult = true);

        // ASSERT
        enumToTest.ShouldSatisfyAllConditions(
            () => actionToTestRunResult.ShouldBeTrue(),
            () => defaultActionRunResult.ShouldBeFalse()
            );
    }

    [Fact]
    public void Condition_ShouldRunDefaultConditionWhenNoConditionIsMet()
    {
        //ARRANGE
        var enumToTest = TestEnumeration.Option3;

        var firstActionRunResult = false;
        var secondActionRunResult = false;
        var defaultActionTestRunResult = false;

        //ACT
        enumToTest.When(TestEnumeration.Option1).Then(() => firstActionRunResult = true)
            .When(TestEnumeration.Option2).Then(() => secondActionRunResult = true)
            .DefaultCondition(() => defaultActionTestRunResult = true);

        //ASSERT
        enumToTest.ShouldSatisfyAllConditions(
            () => firstActionRunResult.ShouldBeFalse(),
            () => secondActionRunResult.ShouldBeFalse(),
            () => defaultActionTestRunResult.ShouldBeTrue()
            );
    }

    [Fact]
    public void Condition_ShouldNotRunOtherConditionsWhenFirstConditionIsMet()
    {
        //ARRANGE
        var enumToTest = TestEnumeration.Option1;

        var firstActionRunResult = false;
        var secondActionRunResult = false;

        //ACT
        enumToTest.When(TestEnumeration.Option1).Then(() => firstActionRunResult = true)
            .When(TestEnumeration.Option2).Then(() => secondActionRunResult = true);

        //ASSERT
        enumToTest.ShouldSatisfyAllConditions(
            () => firstActionRunResult.ShouldBeTrue(),
            () => secondActionRunResult.ShouldBeFalse()
            );
    }

    [Fact]
    public void Condition_ShouldRunForMatchesWithinLastConditionWhenOthersFail()
    {
        //ARRANGE
        var enumToTest = TestEnumeration.Option3;

        var firstActionRunResult = false;
        var secondActionRunResult = false;
        var thirdActionRunResult = false;

        //ACT
        enumToTest.When(TestEnumeration.Option1).Then(() => firstActionRunResult = true)
            .When(TestEnumeration.Option2).Then(() => secondActionRunResult = true)
            .When(TestEnumeration.ReadOnlyEnumerationList).Then(() => thirdActionRunResult = true);

        //ASSERT
        enumToTest.ShouldSatisfyAllConditions(
            () => firstActionRunResult.ShouldBeFalse(),
            () => secondActionRunResult.ShouldBeFalse(),
            () => thirdActionRunResult.ShouldBeTrue()
            );
    }

    [Fact]
    public void Condition_ShouldRunForMatchesLastParameterWhenOthersFail()
    {
        //ARRANGE
        var enumToTest = TestEnumeration.Option3;

        var firstActionRunResult = false;
        var secondActionRunResult = false;
        var thirdActionRunResult = false;

        //ACT
        enumToTest.When(TestEnumeration.Option1).Then(() => firstActionRunResult = true)
            .When(TestEnumeration.Option2).Then(() => secondActionRunResult = true)
            .When(TestEnumeration.Option1, TestEnumeration.Option2, TestEnumeration.Option3).Then(() => thirdActionRunResult = true);

        //ASSERT
        enumToTest.ShouldSatisfyAllConditions(
            () => firstActionRunResult.ShouldBeFalse(),
            () => secondActionRunResult.ShouldBeFalse(),
            () => thirdActionRunResult.ShouldBeTrue()
        );
    }
    #endregion
    #region Comparison Tests
    [Theory]
    [MemberData(nameof(ComparisonEnumerationTheoryData))]
    public void CompareTo_ShouldReturnTheExpectedResult(TestEnumeration lhs, TestEnumeration rhs, Int32 expectedValue)
    {
        //ARRANGE && ACT
        var result = lhs.CompareTo(rhs);

        //ASSERT
        result.ShouldBe(expectedValue);
    }

    [Theory]
    [MemberData(nameof(ComparisonOperatorTheoryData))]
    public void LessThan_ShouldReturnTheExpectedResult(TestEnumeration lhs, TestEnumeration rhs, Boolean lessThan,
        Boolean equalTo, Boolean greaterThan)
    {
        //ARRANGE && ACT
        var result = lhs < rhs;
        //ASSERT
        result.ShouldBe(lessThan);
    }

    [Theory]
    [MemberData(nameof(ComparisonOperatorTheoryData))]
    public void GreaterThan_ShouldReturnTheExpectedResult(TestEnumeration lhs, TestEnumeration rhs, Boolean lessThan,
        Boolean equalTo, Boolean greaterThan)
    {
        //ARRANGE && ACT
        var result = lhs > rhs;
        //ASSERT
        result.ShouldBe(greaterThan);
    }

    [Theory]
    [MemberData(nameof(ComparisonOperatorTheoryData))]
    public void LessThanOrEqualTo_ShouldReturnTheExpectedResult(TestEnumeration lhs, TestEnumeration rhs,
        Boolean lessThan, Boolean equalTo, Boolean greaterThan)
    {
        //ARRANGE && ACT
        var result = lhs <= rhs;
        //ASSERT
        result.ShouldBe(lessThan || equalTo);
    }

    [Theory]
    [MemberData(nameof(ComparisonOperatorTheoryData))]
    public void GreaterThanOrEqualTo_ShouldReturnTheExpectedResult(TestEnumeration lhs, TestEnumeration rhs,
        Boolean lessThan, Boolean equalTo, Boolean greaterThan)
    {
        //ARRANGE && ACT
        var result = lhs >= rhs;
        //ASSERT
        result.ShouldBe(greaterThan || equalTo);
    }
    #endregion
    #region Explicit Conversion Tests
    [Fact]
    public void ExplicitConvert_ShouldReturnEnumerationFromProvidedValue()
    {
        //ARRANGE
        var testValue = 1;

        //ACT
        var result = (TestEnumeration)testValue;

        //ASSERT
        result.ShouldBe(TestEnumeration.Option1);
    }

    [Fact]
    public void ExplicitConvert_ShouldReturnEnumerationFromProvidedNullableValue()
    {
        //ARRANGE
        int? testValue = 1;

        //ACT
        var result = (TestEnumeration)testValue;

        //ASSERT
        result.ShouldBe(TestEnumeration.Option1);
    }

    [Fact]
    public void ExplictConvert_ShouldReturnNullReferenceFromNullValue()
    {
        //ARRANGE
        int? testValue = null;

        //ACT
        var result = (TestEnumeration)testValue;

        //ASSERT
        result.ShouldBeNull();
    }
    #endregion
    #region Implicit Conversion Tests
    [Fact]
    public void ImplicitConversion_ShouldReturnCorrectEnumerationFromGivenValue()
    {
        //ARRANGE
        var testEnumeration = TestEnumeration.ReadOnlyEnumerationList.Random(TestEnumeration.Option1);

        //ACT
        int result = testEnumeration;

        //ASSERT
        result.ShouldSatisfyAllConditions(
            () => result.ShouldBeOfType<Int32>(),
            () => result.ShouldBe(testEnumeration.Value)
        );
    }
    #endregion
    #region Enumeration With String Key Tests
    [Theory]
    [MemberData(nameof(StringEnumerationPairTheoryData))]
    public void FromValue_ShouldReturnEnumerationMatchingTheProvidedValue(String testValue, TestEnumWithStringKey expectedEnumWithStringKey)
    {
        //ARRANGE && ACT
        var result = TestEnumWithStringKey.ParseFromValue(testValue);

        //ASSERT
        result.ShouldSatisfyAllConditions(
            () => result.ShouldBeOfType(typeof(TestEnumWithStringKey)),
            () => result.ShouldBe(expectedEnumWithStringKey)
        );
    }

    [Fact]
    public void FromValue_ShouldThrowWhenNoMatchingValueIsFound()
    {
        //ARRANGE
        var testValue = String.Empty;

        //ACT && ASSERT
        Should.Throw<KeyNotFoundException>(() => TestEnumWithStringKey.ParseFromValue(testValue));
    }

    [Fact]
    public void FromValue_ShouldReturnProvidedDefaultValueWhenGivenValueIsNotMatched()
    {
        //ARRANGE
        var testValue = String.Empty;
        var defaultTestValue = TestEnumWithStringKey.Option1;

        //ACT
        var result = TestEnumWithStringKey.ParseFromValue(testValue, defaultTestValue);

        //ASSERT
        result.ShouldBeOfType<TestEnumWithStringKey>();
        result.ShouldBe(defaultTestValue);
    }
    #endregion
    #region Enumeration Parse by Key tests
    [Fact]
    public void Parse_ShouldReturnTheEnumMatchingTheGivenValue()
    {
        //ARRANGE
        var expectEnum = TestEnumeration.ReadOnlyEnumerationList.Random(TestEnumeration.Option1);
        var testName = expectEnum.Name;

        //ACT
        var result = TestEnumeration.Parse(testName);

        //ASSERT
        result.ShouldBe(expectEnum);
    }

    [Fact]
    public void Parse_ShouldThrowWhenGivenNameIsEmpty()
    {
        //ARRANGE
        var testName = String.Empty;

        //ACT && ASSERT
        Should.Throw<ArgumentNullException>(() => TestEnumeration.Parse(testName));
    }

    [Fact]
    public void Parse_ShouldThrowWhenGivenNameIsNull()
    {
        //ARRANGE & ACT && ASSERT
        Should.Throw<ArgumentNullException>(() => TestEnumeration.Parse(null));
    }

    [Fact]
    public void ParseShouldThrowWhenGivenKeyIsNotMatched()
    {
        //ARRANGE
        var key = RandomData.GetAlphaNumericString();

        //ACT && ASSERT
        Should.Throw<KeyNotFoundException>(() => TestEnumeration.Parse(key));
    }
    #endregion
    #region Enumeration TryParse By Key Tests
    [Fact]
    public void TryParse_ShouldReturnTrueWithMatchingEnumFromGivenKey()
    {
        //ARRANGE
        var expectedEnumeration = TestEnumeration.ReadOnlyEnumerationList.Random(TestEnumeration.Option1);

        var testName = expectedEnumeration.Name;

        //ACT
        var (result, enumeration) = TestEnumeration.TryParse(testName, false);

        //ASSERT
        result.ShouldSatisfyAllConditions(
            () => result.ShouldBeTrue(),
            () => enumeration.ShouldBe(expectedEnumeration)
            );
    }

    [Fact]
    public void TryParse_ShouldReturnFalseResultWhenGivenKeyIsNotFound()
    {
        //ARRANGE
        var testEnumeration = String.Empty;

        //ACT
        var (result, enumeration) = TestEnumeration.TryParse(testEnumeration, false);

        //ASSERT
        result.ShouldSatisfyAllConditions(
            () => result.ShouldBeFalse(),
            () => enumeration.ShouldBeNull()
        );
    }

    [Fact]
    public void TryParse_ShouldReturnFalseResultWhenGivenKeyIsNull()
    {
        //ARRANGE && ACT
        var (result, enumeration) = TestEnumeration.TryParse(null, false);

        //ASSERT
        result.ShouldSatisfyAllConditions(
            () => result.ShouldBeFalse(),
            () => enumeration.ShouldBeNull()
        );
    }
    #endregion
    #region Derived Enumeration Tests
    [Fact]
    public void ReadOnlyEnumList_ShouldReturnAllDerivedAndBaseEnumerations()
    {
        //ARRANGE
        var expected = new[]
        {
            TestDerivedEnum.DerivedOption1,
            TestDerivedEnum.DerivedOption2,
            TestDerivedEnum.DerivedOption3,
            TestEnumeration.Option1,
            TestEnumeration.Option2,
            TestEnumeration.Option3
        };

        //ACT
        var result = TestDerivedEnum.ReadOnlyEnumerationList;

        //ASSERT
        result.ShouldBe(expected);
    }
    #endregion
}