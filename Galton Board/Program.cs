using System.Runtime.InteropServices;

//Console.WriteLine("Hello, World!");

List<ball> balls = new List<ball>();
double CurrentTry = 0;

int passCount = 0;

while(true)
{
    CurrentTry++;

    balls.Clear();
    for (int i = 0; i < 10; i++)
    {
        ball temp = new ball();
        temp.GoToBottom();
        balls.Add(temp);
    }

    int chosenVerticalPos = balls[0].verticalPos;
    bool allEqual = true;

    foreach (ball ball in balls)
    {
        if (ball.verticalPos != chosenVerticalPos)
        {
            allEqual = false;
            break;
        }
    }

    if(allEqual)
    {
        passCount++;

        Console.WriteLine("Current try: " + CurrentTry);
        Console.WriteLine("Pass count: " + passCount);

        if(passCount == 1000)
        {
            break;
        }
    }
    //Console.WriteLine("-----------------------------------");
}

class ball
{
    public int verticalPos = 0;
    int horizontalPos = 0;

    public void GoToBottom()
    {
        for (int horizontalPos = 0; horizontalPos < 5; horizontalPos++)
        {
            if(Random())
                verticalPos++;
            else
                verticalPos--;

            //Console.WriteLine("horizontal: " + horizontalPos + ", vertical: " + verticalPos);
        }
        //Console.WriteLine("////////");
    }
    bool Random()
    {
        Random rnd = new Random();
        if(rnd.Next(0, 2) == 0)
            return true;
        else
            return false;
    }
}