using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class MathExpression
    {
        enum Operation { PLUS, MINUS, MULT, DIV, POW };
        enum Type { VARX,VARY, CONST, FUNC, OPERATION, NONE };
        enum Func { SIN, COS, LN, EXP };

        private MathExpression left;
        private MathExpression right;
        private Type t;
        private Operation op;
        private double cons;
        private Func f;

       // private 

        public MathExpression()
        {
            left = null;
            right = null;
        }

        private static int prior(char c)
        {
            if (c == '+' || c == '-')
                return 0;
            if (c == '*' || c == '/')
                return 1;
            if (c == '^')
                return 2;
            if (c == 's' || c == 'c' || c == 'l')
                return 3;
            if (c == 't')
                return 4;
            else
                return 4;
        }

        private static bool is_op(char c)
        {
            if (c == '+' || c == '-' || c == '*' || c == '/' || c == '^')
                return true;
            else
                return false;
        }

        private static Type getType(String s, int ind) // определение типа символа
        {
            if (is_op(s[ind]))
                return Type.OPERATION;
            if ((s.Length > ind + 3) && (s.Substring(ind,3) == "sin" || s.Substring(ind,3) == "cos" || s.Substring(ind,3) =="log"))
                return Type.FUNC;
            if (s[ind] == 'x')
                return Type.VARX;
            if (s[ind] == 'y')
                return Type.VARY;
            if ((int)s[ind] >= (int)'0' && (int)s[ind] <= (int)'9')
                return Type.CONST;
            if (s[ind] == 'e' || s[ind] == 'E')
                return Type.CONST;
            if ((s.Length >= ind + 2) && (s.Substring(ind,2).ToLower() =="pi"))
                return Type.CONST;
            else
                return Type.NONE;
        }

       private static Func getFunc(String s, int ind)
        {
            if (s.Substring(ind, 3) == "sin")
                return Func.SIN;
            if (s.Substring(ind, 3) == "cos")
                return Func.COS;
            if (s.Substring(ind, 3) == "log")
                return Func.LN;
            if (s.Substring(ind, 3) == "exp")
                return Func.EXP;
            throw new Exception("Невозможно распознать функцию.");
        }

       private static Operation getOperation(String s, int ind)
       {
           if (s[ind] == '+')
               return Operation.PLUS;
           if (s[ind] == '-')
               return Operation.MINUS;
           if (s[ind] == '*')
               return Operation.MULT;
           if (s[ind] == '/')
               return Operation.DIV;
           if (s[ind] == '^')
               return Operation.POW;
           else
               throw new Exception("Невозможно распознать операцию.");
       }

       private static double getConst(String s, int ind)
       {
           if (s[ind] == 'e' || s[ind] == 'E')
               return Math.E;
           else if ((s.Length >= ind + 2) && (s.Substring(ind,2).ToLower() =="pi"))
               return Math.PI;
           else
               return Convert.ToDouble(s);
       }

       public static MathExpression parseString(String s)
       {
           int minBr = int.MaxValue;
           MathExpression res = new MathExpression();
           int last = 0;
           while (minBr > 0)
           {
               int brackets = 0;
               last = 0;
               char cur, prev = '(';
               int p = 10;
               for (int i = 0; i < s.Length; i++) // цикл по строке
               {
                   cur = s[i];
                   if (cur == ' ')
                       continue;
                   if (cur == '(')
                       brackets++;
                   if (cur == ')')
                       brackets--;
                   if ((i != s.Length - 1) || (s.Length == 1))
                       minBr = Math.Min(minBr, brackets);
                   if (brackets == 0)
                   {
                       if (p >= prior(cur))
                       {
                           if ((prior(cur) == 0 && prev != '(') || (prior(cur) != 0))
                           {
                               if (getType(s, i) != Type.NONE)
                               {
                                   p = prior(cur);
                                   last = i;
                               }
                           }
                           else
                           {
                               String tmp = "";
                               for (int j = 0; j < i; j++)
                                   tmp += s[j];
                               tmp += '0';
                               for (int j = i; j < s.Length; j++)
                                   tmp += s[j];
                               prev = '0';
                               s = tmp;
                               continue;
                           }
                       }
                   }
                   prev = cur;
               }
               if (minBr != 0)
               {
                   String newS = "";
                   for (int i = 1; i < s.Length - 1; i++)
                       newS += s[i];
                   s = newS;
               }
           }
           res.t = getType(s, last);
           if (res.t == Type.FUNC)
           {
               res.f = getFunc(s, last);
               String newS = "";
               for (int i = 3; i < s.Length; i++)
                   newS += s[i];
               res.left = null;
               res.right = parseString(newS);
           }
           else if (res.t == Type.OPERATION)
           {
               res.op = getOperation(s, last);
               String ls = "", rs = "";
               for (int i = 0; i < last; i++)
                   ls += s[i];
               for (int i = last + 1; i < s.Length; i++)
                   rs += s[i];
               res.left = parseString(ls);
               res.right = parseString(rs);
           }
           else if (res.t == Type.CONST)
               res.cons = getConst(s, last);
           return res;
       }


       public double getValue(double varx, double vary)
       {
           if (t == Type.CONST)
               return cons;
           else if (t == Type.VARX)
               return varx;
           else if (t == Type.VARY)
               return vary;
           else if (t == Type.OPERATION)
           {
               if (op == Operation.PLUS)
                   return left.getValue(varx, vary) + right.getValue(varx, vary);
               else if (op == Operation.MINUS)
                   return left.getValue(varx, vary) - right.getValue(varx, vary);
               else if (op == Operation.MULT)
                   return left.getValue(varx, vary) * right.getValue(varx, vary);
               else if (op == Operation.DIV)
                   return left.getValue(varx, vary) / right.getValue(varx, vary);
               else if (op == Operation.POW)
                   return Math.Pow(left.getValue(varx, vary), right.getValue(varx, vary));
           }
           else if (t == Type.FUNC)
           {
               if (f == Func.SIN)
                   return Math.Sin(right.getValue(varx, vary));
               else if (f == Func.COS)
                   return Math.Cos(right.getValue(varx, vary));
               else if (f == Func.LN)
                   return Math.Log(right.getValue(varx, vary));
           }
           throw new Exception("Невозможно распознать функцию");
       }

    }
}
