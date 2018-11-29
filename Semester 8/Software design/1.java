package test;
public class HelloWorld {
	public static boolean isDouble(Object x)
	{
		return x instanceof Double;
	}
	public static String[] task(Object[]arr)
	{
		String[]out=new String[arr.length];
		for(int i=0;i<arr.length;i++) 
		{
		if(isDouble(arr[i]))
		{
			out[i]="Decimal";
		}
		else
		{
			out[i]="String";
		}
		}
		return out;
	}
	
	public static void main(String[] args) {
System.out.print("Л/р № 1 от Паши\n");
Object[]arr=new Object[6];
arr[0]="name";
arr[1]=2;
arr[2]=true;
arr[3]=-2.5;
arr[4]=.2;
arr[5]="+";
System.out.print("Массив с аргументами, которые подаются на вход:\n");
for(Object x:arr)
{
	System.out.print(x+" ");
}
System.out.print("\n");
for(String str:task(arr))
{
	System.out.print(str+"\n");
}
	}
}