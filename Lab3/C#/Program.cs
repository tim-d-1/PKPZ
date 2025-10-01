string text = "big brown bear bites bread but not butter";
char[] arr = text.ToCharArray();
string[] words = new string(arr).Split(new char[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
int count = 0;
foreach (string w in words)
    if (w.Length > 0 && w[0] == 'b') count++;
Console.WriteLine(count);