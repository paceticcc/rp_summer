using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib.Tests;
public class PhoneNumberTests
{
    [Theory]
    [MemberData(nameof(WithoutExtValidData))]
    public void Parse_valid_data_without_ext(string input, string expectedNumber, string expectedExtension, string expectedPhoneNumber)
    {
        PhoneNumber phoneNumber = new PhoneNumber(input);
        Assert.Equal(expectedNumber, phoneNumber.Number());
        Assert.Equal(expectedExtension, phoneNumber.Extension());
        Assert.Equal(expectedPhoneNumber, phoneNumber.ToString());
    }

    public static TheoryData<string, string, string, string> WithoutExtValidData()
    {
        return new TheoryData<string, string, string, string>
        {
            { "+7 (8362) 45-02-72", "+78362450272", "", "+78362450272" },
            { "7 (8362) 45-02-72", "+78362450272", "", "+78362450272" },
            { "8 (8362) 45-02-72", "+88362450272", "", "+88362450272" },
            { "+1 (234) 567-8901", "+12345678901", "", "+12345678901" },
            { "1-234-567-8901", "+12345678901", "", "+12345678901" },
            { "+44 20 7946 0958", "+442079460958", "", "+442079460958" },
            { "442079460958", "+442079460958", "", "+442079460958" },
        };
    }

    [Theory]
    [MemberData(nameof(WithExtValidData))]
    public void Parse_valid_data_with_ext(string input, string expectedNumber, string expectedExtension, string expectedPhoneNumber)
    {
        PhoneNumber phoneNumber = new PhoneNumber(input);
        Assert.Equal(expectedNumber, phoneNumber.Number());
        Assert.Equal(expectedExtension, phoneNumber.Extension());
        Assert.Equal(expectedPhoneNumber, phoneNumber.ToString());
    }

    public static TheoryData<string, string, string, string> WithExtValidData()
    {
        return new TheoryData<string, string, string, string>
        {
            { "1-234-567-8901 x1234", "+12345678901", "1234", "+12345678901x1234" },
            { "+7 (8362) 45-02-72x567", "+78362450272", "567", "+78362450272x567" },
            { "+1 (800) 123-4567 x9999", "+18001234567", "9999", "+18001234567x9999" },
            { "+7 (8362) 45-02-72x5", "+78362450272", "5", "+78362450272x5" },
        };
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public void Invalid_argument_exception(string input)
    {
        Assert.Throws<ArgumentException>(() => new PhoneNumber(input));
    }

    public static TheoryData<string> InvalidData()
    {
        return new TheoryData<string>
        {
            { "abc" },
            { "12345678901234567890" },
            { "1-234-567-8901 x" },
            { "1-234-567-8901 xabc" },
        };
    }

    [Fact]
    public void Argument_null_exception()
    {
        Assert.Throws<ArgumentNullException>(() => { PhoneNumber phoneNumber = new PhoneNumber(""); });
        Assert.Throws<ArgumentNullException>(() => { PhoneNumber phoneNumber = new PhoneNumber(null); });
    }

    [Fact]
    public void Min_phone_number()
    {
        PhoneNumber phoneNumber1 = new PhoneNumber("+12");
        PhoneNumber phoneNumber2 = new PhoneNumber("12");
        Assert.Equal(phoneNumber1.ToString(), "+12");
        Assert.Equal(phoneNumber2.ToString(), "+12");
    }

    [Fact]
    public void Max_phone_number()
    {
        PhoneNumber phoneNumber1 = new PhoneNumber("+123456789012345");
        PhoneNumber phoneNumber2 = new PhoneNumber("123456789012345");
        Assert.Equal(phoneNumber1.ToString(), "+123456789012345");
        Assert.Equal(phoneNumber2.ToString(), "+123456789012345");
    }

    [Fact]
    public void Min_ext()
    {
        PhoneNumber phoneNumber = new PhoneNumber("+1234567890 x1");
        Assert.Equal(phoneNumber.ToString(), "+1234567890x1");
        Assert.Equal(phoneNumber.Extension(), "1");
        Assert.Equal(phoneNumber.Number(), "+1234567890");
    }

    [Fact]
    public void Max_ext()
    {
        PhoneNumber phoneNumber = new PhoneNumber("+1234567890 x1234");
        Assert.Equal(phoneNumber.ToString(), "+1234567890x1234");
        Assert.Equal(phoneNumber.Extension(), "1234");
        Assert.Equal(phoneNumber.Number(), "+1234567890");
    }
}