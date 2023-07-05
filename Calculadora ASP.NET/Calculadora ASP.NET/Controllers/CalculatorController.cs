using Microsoft.AspNetCore.Mvc;

namespace Calculadora_ASP.NET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        #region API functions

        [HttpGet]
        public IActionResult Home()
        {
            var s =
                "Usage: .../calculator/{parameter}/{firstNumber}/{secondNumber (if Needed)}\n\n" +
                "Parameter:\n" +
                "/sum : Sum 2 numbers\n" +
                "/sub : Subtracts 2 numbers\n" +
                "/mult : Multiply 2 numbers\n" +
                "/div: Divide 2 numbers\n" +
                "/mean: Mean of 2 numbers\n" +
                "/sqrt: Square root of 1 number\n";

            return Ok(s);
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            var first = ConvertNumber(firstNumber);
            var sec = ConvertNumber(secondNumber);

            if(CheckErrors(first.Item2, sec.Item2))
                return Ok((first.Item1 + sec.Item1).ToString());

            return BadRequest();
        }

        [HttpGet("sub/{firstNumber}/{secondNumber}")]
        public IActionResult Sub(string firstNumber, string secondNumber)
        {
            var first = ConvertNumber(firstNumber);
            var sec = ConvertNumber(secondNumber);

            if (CheckErrors(first.Item2, sec.Item2))
                return Ok((first.Item1 - sec.Item1).ToString());

            return BadRequest();
        }

        [HttpGet("mult/{firstNumber}/{secondNumber}")]
        public IActionResult Mult(string firstNumber, string secondNumber)
        {
            var first = ConvertNumber(firstNumber);
            var sec = ConvertNumber(secondNumber);

            if (CheckErrors(first.Item2, sec.Item2))
                return Ok((first.Item1 * sec.Item1).ToString());

            return BadRequest();
        }

        [HttpGet("div/{dividend}/{divisor}")]
        public IActionResult Div(string dividend, string divisor)
        {
            var first = ConvertNumber(dividend);
            var sec = ConvertNumber(divisor);

            if (CheckErrors(first.Item2, sec.Item2) && sec.Item1 != 0)
                return Ok((first.Item1 / sec.Item1).ToString());

            return BadRequest();
        }

        [HttpGet("mean/{firstNumber}/{secondNumber}")]
        public IActionResult Mean(string firstNumber, string secondNumber)
        {
            var first = ConvertNumber(firstNumber);
            var sec = ConvertNumber(secondNumber);

            if (CheckErrors(first.Item2, sec.Item2))
                    return Ok(((first.Item1 + sec.Item1)/2).ToString());

            return BadRequest();
        }

        [HttpGet("sqrt/{number}")]
        public IActionResult SquareRoot(string number)
        {
            var op = ConvertNumber(number);

            if (CheckErrors(op.Item2) && op.Item1 >= 0)
                return Ok(((decimal)Math.Sqrt((double)op.Item1)).ToString());

            return BadRequest();
        }

        #endregion

        #region Utility Methods

        private bool CheckErrors(params string[] strings)
        {
            foreach (var s in strings)
                if (s != string.Empty) return false;

            return true;
        }

        public Tuple<decimal, string> ConvertNumber(string number)
        {
            if (IsNumeric(number)) 
                return new Tuple<decimal, string> (Convert.ToDecimal(number), "");

            return new Tuple<decimal, string>(0, "Argument cannot be converted to decimal.");
        }

        private bool IsNumeric(string number)
        {
            return decimal.TryParse(number, out _);
        }
        #endregion
    }
}