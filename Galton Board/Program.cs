using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

var watch = Stopwatch.StartNew();//

int ballCount = 5;
int layerCount = 18;//18 = 171pins

Random random = new Random();

Task[] tasks = new Task[40];
ConcurrentBag<double> TryCounts = new ConcurrentBag<double>();

ballCount--;

for (int i = 0; i < tasks.Length; i++)
{
    tasks[i] = Task.Run(() =>
    {
        TryCounts.Add(CreateBallsAndFell());
        //Console.WriteLine("Thread finished");
        //Console.WriteLine("=============================");
    });
}

Task.WaitAll(tasks);

//int j = 0;
//foreach (int tryCount in TryCounts)
//{
//    j++;
//    Console.WriteLine("Thread" + TurnNumberToString(j) + ": " + tryCount);
//}

watch.Stop();
Console.WriteLine($"The Execution time of the program is {watch.ElapsedMilliseconds}ms");//349ms, 375ms, 454ms, 406ms, 367ms

while (true)
{
    Console.ReadLine();
}

//---------------------

string TurnNumberToString(int input)
{
    string output = "";

    if(input < 10)
    {
        output = "0" + input;
    }
    else
    {
        output = input + "";
    }
    return output;
}
double CreateBallsAndFell()
{
    double CurrentTry = 0;
    int PassCount = 0;

    while (true)
    {
        CurrentTry++;

        ball firstBall = new ball(layerCount, random);
        firstBall.GoToBottom();

        int DecidedVerticalPos = firstBall.verticalPos;
        bool allEqual = true;

        for (int i = 0; i < ballCount; i++)
        {
            ball temp = new ball(layerCount, random);
            temp.GoToBottom();
            if (temp.verticalPos != DecidedVerticalPos)
            {
                allEqual = false;
                break;
            }
        }

        if (allEqual)
        {
            PassCount++;

            Console.WriteLine("Current try: " + CurrentTry);
            Console.WriteLine("Pass count: " + PassCount);
            Console.WriteLine("Position: " + DecidedVerticalPos);
            Console.WriteLine("----");

            if (PassCount == 25)
            {
                return CurrentTry;
            }
        }
    }
}

class ball
{
    public int verticalPos = 0;

    int layerCount = 0;//18 = 171 pins
    int rndNumber;

    public ball(int layerCount, Random rnd)
    {
        this.layerCount = layerCount;
        rndNumber = rnd.Next();
    }

    public void GoToBottom()
    {
        var bitArray = new BitArray(BitConverter.GetBytes(rndNumber));//3456213 = 010110010010011100101

        for (int horizontalPos = 0; horizontalPos < layerCount; horizontalPos++)
        {
            if(bitArray.Get(horizontalPos + 1))
                verticalPos++;
            else
                verticalPos--;

            //Console.WriteLine("horizontal: " + horizontalPos + ", vertical: " + verticalPos);
        }
        //Console.WriteLine("////////");
    }
}