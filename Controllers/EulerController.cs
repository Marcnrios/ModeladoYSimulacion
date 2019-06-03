using System;
using System.Collections.Generic;
using Flee.PublicTypes;
using Microsoft.AspNetCore.Mvc;

namespace Euler.Controllers
{
    public class EulerController : Controller
    {
        static float func(float xt, float t, string function)
        {
            var expression = CreateExpressionForTX(function);
            return (float)expression((double)xt, (double)t);
        }

        [HttpGet()]
        public IActionResult Euler() => View(new ParametersViewModel());

        [HttpPost()]
        public IActionResult Euler(ParametersViewModel pvm)
        {
            var viewModel = new ParametersViewModel();
            var list = new List<Point>();
            var xt = pvm.xt;
            var t = pvm.a;
            float predictor;
            Console.WriteLine($"Cuando t es: {t} / x es: {xt}");
            list.Add(new Point { x = t, y = xt });
            while (t < pvm.b)
            {
                predictor = xt + pvm.h * func(xt, t, pvm.function);
                xt = xt + (0.5f) * (func(xt, t, pvm.function) + func(predictor, t + pvm.h, pvm.function)) * pvm.h;
                t = t + pvm.h;
                Console.WriteLine($"Cuando t es: {t} / x es: {xt}");
                list.Add(new Point { x = (float)Math.Round(t,2), y = xt });
            }
            viewModel.EulerMejoradoPoints = list;
            var listEuler = EulerOrden1(pvm);
            viewModel.EulerPoints = listEuler;
            return View(viewModel);
        }

        private List<Point> EulerOrden1(ParametersViewModel pvm)
        {
            var list = new List<Point>();
            var xt = pvm.xt;
            var t = pvm.a;
            list.Add(new Point { x = t, y = xt });
            while (t < pvm.b)
            {
                xt = xt + pvm.h * func(xt, t, pvm.function);
                t = t + pvm.h;
                Console.WriteLine($"Cuando t es: {t} / x es: {xt}");
                list.Add(new Point { x = (float)Math.Round(t,2), y = xt });
            }
            return list;
        }

        public static Func<double, double, double> CreateExpressionForTX(string expression)
        {
            ExpressionContext context = new ExpressionContext();
            context.Variables["x"] = 0.0d;
            context.Variables["t"] = 0.0d;
            IDynamicExpression e = context.CompileDynamic(expression);
            Func<double, double, double> expressionEvaluator = (double input, double input2) =>
            {
                context.Variables["x"] = input;
                context.Variables["t"] = input2;
                var result = (double)e.Evaluate();
                return result;
            };
            return expressionEvaluator;
        }
    }
}
