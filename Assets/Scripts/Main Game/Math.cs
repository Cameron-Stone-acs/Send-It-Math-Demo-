using UnityEngine;
using TMPro;
using System;
using UnityEditor.Timeline.Actions;

public class Math : MonoBehaviour
{
    // Can change phase inside inspector to see other phases,
    // no need to increase score.
    public float damage = 1;
    public int phase = 1;
    public int score;
    public float qAnswer;
    public int requiredCount = 10;

    // All text boxes used for UI management
    public TMP_Text equationText;
    public TMP_Text scoreText;
    public TMP_InputField answerText;

    // Other scripts / gameobjects
    public GameObject enemySpawner;
    private GameObject player;
    private GameObject numberSpiral;

    public AudioClip correct1;
    public AudioClip correct2;
    public AudioClip phaseUp;
    
    public AudioSource sound;

    // Can make private if need be, didn't see a use to see them
    // in inspector once they were confirmed to work
    
    private string equation;
    private string input;
    private float min;
    private float max;
    private int count;

    // Starts of equations
    public void Start()
    {
        player = GameObject.Find("Player");
        enemySpawner = GameObject.Find("Enemy Spawner");
        numberSpiral = GameObject.Find("Number Spiral");
        run();
    }

    // Basically the for loop but works for specifically what we need.
    // Can change "10" to an int for longer phases
    public void run()
    {
        if (count == requiredCount)
        {
            sound.clip = phaseUp;
            sound.pitch = UnityEngine.Random.Range(0.74f, 1.26f);
            sound.Play();
            count = 0;
            phase++;
        }
        CreateEquation();
    }

    public void CreateEquation()
    {
        int a;

        if (phase == 1)
        {
            min = 0; max = 2;
            Addition();
        }
        if (phase == 2)
        {
            min = 0; max = 10;
            Addition();
        }
        if (phase == 3)
        {
            min = 0; max = 10;
            Subtract(false, false);
        }
        if (phase == 4)
        {
            min = 0; max = 10;
            Subtract(true, true);
        }
        if (phase == 5)
        {
            a = UnityEngine.Random.Range(1, 3);
            min = 0; max = 10;
            if (a == 1) Addition();
            if (a == 2) Subtract(true, false);
        }
        if (phase == 6)
        {
            a = UnityEngine.Random.Range(1, 3);
            min = 0; max = 100;
            if (a == 1) Addition();
            if (a == 2) Subtract(true, false);
        }
        if (phase == 7)
        {
            min = 0; max = 10;
            Multiplication(true, false);
        }
        if (phase == 8)
        {
            min = 0; max = 100;
            Multiplication(true, false);
        }
        if (phase == 9)
        {
            a = UnityEngine.Random.Range(1, 4);
            min = 0; max = 100;
            if (a == 1) Addition();
            if (a == 2) Subtract(false, false);
            if (a == 3) Multiplication(true, false);
        }
        if (phase == 10)
        {
            min = 0; max = 10;
            parentheses(true, false);
        }
        if (phase == 11)
        {
            a = UnityEngine.Random.Range(1, 5);
            min = 0; max = 100;
            if (a == 1) Addition();
            if (a == 2) Subtract(false, false);
            if (a == 3) Multiplication(true, false);
            if (a == 4) parentheses(true, false);
        }
        if (phase == 12)
        {
            min = 0; max = 1000;
            Addition();
        }
        if (phase == 13)
        {
            a = UnityEngine.Random.Range(1, 3);
            min = 0; max = 1000;
            if (a == 1) Addition();
            if (a == 2) Subtract(true, false);
        }
        if (phase == 14)
        {
            min = 0; max = 100;
            Multiplication(false, true);
        }
        if (phase == 15)
        {
            min = 0; max = 10;
            Division();
        }
        if (phase == 16)
        {
            min = 0; max = 100;
            Division();
        }
        if (phase == 17)
        {
            min = 0; max = 1000;
            parentheses(false, false);
        }
        if (phase == 18)
        {
            a = UnityEngine.Random.Range(1, 6);
            min = 0; max = 1000;
            if (a == 1) Addition();
            if (a == 2) Subtract(false, false);
            if (a == 3) Multiplication(true, false);
            if (a == 4) parentheses(true, false);
            if (a == 5) Division();
        }
        if (phase == 19)
        {
            a = UnityEngine.Random.Range(1, 6);
            min = 0; max = 1000;
            if (a == 1) Addition();
            if (a == 2) Subtract(false, false);
            if (a == 3) Multiplication(false, false);
            if (a == 4) parentheses(false, false);
            if (a == 5) Division();
        }
        if (phase == 20)
        {
            min = 0; max = 10;
            PEMDAS();
        }
        if (phase == 21)
        {
            min = 0; max = 100;
            PEMDAS();
        }
        if (phase >= 22)
        {
            a = UnityEngine.Random.Range(1, 7);
            min = 0; max = 1000;
            if (a == 1) Addition();
            if (a == 2) Subtract(false, false);
            if (a == 3) Multiplication(true, false);
            if (a == 4) parentheses(true, false);
            if (a == 5) Division();
            if (a == 6) PEMDAS();
        }
        equationText.text = equation;
    }

    // Checks the input from the user and subtracts or 
    // adds to their score based off input
    public void CheckAnswer()
    {
        input = answerText.text;
        EnemySpawner enemyScript = enemySpawner.GetComponent<EnemySpawner>();
        Player playerScript = player.GetComponent<Player>();
        NumberSpiral spiralScript = numberSpiral.GetComponent<NumberSpiral>();

        if (input == qAnswer.ToString())
        {
            int a = UnityEngine.Random.Range(0, 2);
            if (a == 0) sound.clip = correct1;
            else if (a == 1) sound.clip = correct2;
            sound.pitch = UnityEngine.Random.Range(0.74f, 1.26f);
            sound.Play();
            count++;
            score++;
            answerText.text = "";
            playerScript.Attack();
            enemyScript.answerWright();
            spiralScript.SpawnTextBox();
            enemyScript.health = enemyScript.health - damage;
        }
        else
        {
            answerText.text = "";
            count--;
            score--;
            enemyScript.answerWrong();
        }
        scoreText.text = "Score: " + score;
        run();
    }
    public void SkipQuestion()
    {
        EnemySpawner enemyScript = enemySpawner.GetComponent<EnemySpawner>();
        enemyScript.answerWrong();
    }

    // Addition equations
    public void Addition()
    {
        int a = (int)UnityEngine.Random.Range(min, max);
        int b = (int)UnityEngine.Random.Range(min, max);
        qAnswer = a + b;
        equation = a + " + " + b;
    }

    // Subtraction equations
    public void Subtract(bool negative, bool allNeg)
    {
        int a = (int)UnityEngine.Random.Range(min, max);
        int b = (int)UnityEngine.Random.Range(min, max);

        if (!negative && a - b < 0)
        {
            while (a - b < 0)
            {
                a = (int)UnityEngine.Random.Range(min, max);
                b = (int)UnityEngine.Random.Range(min, max);
            }
        }

        if (allNeg && a - b >= 0)
        {
            while (a - b >= 0)
            {
                a = (int)UnityEngine.Random.Range(min, max);
                b = (int)UnityEngine.Random.Range(min, max);
            }
        }
        qAnswer = a - b;
        equation = a + " - " + b;
    }

    public void Multiplication(bool below1k, bool above1k)
    {
        int a = (int)UnityEngine.Random.Range(min, max);
        int b = (int)UnityEngine.Random.Range(min, max);
        if (below1k && a * b > 1000)
        {
            while (a * b > 1000)
            {
                a = (int)UnityEngine.Random.Range(min, max);
                b = (int)UnityEngine.Random.Range(min, max);
            }
        }
        if(above1k && a * b < 1000)
        {
            while (a * b < 1000)
            {
                a = (int)UnityEngine.Random.Range(min, max);
                b = (int)UnityEngine.Random.Range(min, max);
            }
        }
        qAnswer = a * b;
        equation = a + " x " + b;
    }

    // Currently, if "b" = 0, the answer is infinity. Could fix to undefined or 0
    public void Division()
    {
        float a = (int)UnityEngine.Random.Range(min, max);
        float b = (int)UnityEngine.Random.Range(min, max);
        if (b == 0)
        {
            b = 1;
        }
        float c = a / b;
        qAnswer = (float)System.Math.Round(c, 2);
        equation = a + " / " + b;
    }

    public void parentheses(bool below1k, bool above1k)
    {
        float a = (int)UnityEngine.Random.Range(min, max);
        float b = (int)UnityEngine.Random.Range(min, max);
        float c = (int)UnityEngine.Random.Range(min, max);

        // Define possible operations and their string representations
        (Func<float, float, float> op, string symbol)[] ops = new (Func<float, float, float>, string)[]
        {
            ((x, y) => x + y, "+"),
            ((x, y) => x - y, "-"),
            ((x, y) => x * y, "*"),
        };

        // Randomly pick outer op (either +, -, or *)
        int outer = UnityEngine.Random.Range(0, ops.Length);
        int inner = UnityEngine.Random.Range(0, 2);

        // Calculate inner result (either b + c or b - c)
        float innerResult = inner == 0 ? b + c : b - c;
        float rawAnswer = ops[outer].op(a, innerResult);
        qAnswer = (float)System.Math.Round(rawAnswer, 2);
        if (below1k && qAnswer > 1000)
        {
            while (qAnswer > 1000)
            {
                a = (int)UnityEngine.Random.Range(min, max);
                b = (int)UnityEngine.Random.Range(min, max);
                c = (int)UnityEngine.Random.Range(min, max);
                innerResult = inner == 0 ? b + c : b - c;
                rawAnswer = ops[outer].op(a, innerResult);
                qAnswer = (float)System.Math.Round(rawAnswer, 2);
            }
        }
        if(above1k && qAnswer < 1000)
        {
            while (qAnswer < 1000)
            {
                a = (int)UnityEngine.Random.Range(min, max);
                b = (int)UnityEngine.Random.Range(min, max);
                c = (int)UnityEngine.Random.Range(min, max);
                innerResult = inner == 0 ? b + c : b - c;
                rawAnswer = ops[outer].op(a, innerResult);
                qAnswer = (float)System.Math.Round(rawAnswer, 2);
            }
        }
        string innerSymbol = inner == 0 ? "+" : "-";
        // Format equation
        equation = $"{a} {ops[outer].symbol} ({b} {innerSymbol} {c})";
    }

    public void PEMDAS()
    {
        int a = (int)UnityEngine.Random.Range(min, max);
        int b = (int)UnityEngine.Random.Range(min, max);
        int c = (int)UnityEngine.Random.Range(min, max);

        int op1 = UnityEngine.Random.Range(0, 4);
        int op2 = UnityEngine.Random.Range(0, 4);

        // Helper to get operation string and calculate
        float ApplyOp(float x, float y, int op)
        {
            switch (op)
            {
                case 0: return x + y;
                case 1: return x - y;
                case 2: return x * y;
                case 3: return y == 0 ? x : x / y;
                default: return 0;
            }
        }

        string OpToStr(int op) => op == 0 ? "+" : op == 1 ? "-" : op == 2 ? "*" : "/";

        bool parensRight = UnityEngine.Random.Range(0, 2) == 0;

        if (parensRight)
        {
            float right = ApplyOp(b, c, op2);
            float rawAnswer = ApplyOp(a, right, op1);
            qAnswer = (float)System.Math.Round(rawAnswer, 2);
            equation = $"{a} {OpToStr(op1)} ({b} {OpToStr(op2)} {c})";
        }
        else
        {
            float left = ApplyOp(a, b, op1);
            float rawAnswer = ApplyOp(left, c, op2);
            qAnswer = (float)System.Math.Round(rawAnswer, 2);
            equation = $"({a} {OpToStr(op1)} {b}) {OpToStr(op2)} {c}";
        }
    }
}