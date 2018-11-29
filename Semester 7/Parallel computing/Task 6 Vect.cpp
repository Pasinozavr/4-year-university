#include <iostream>
#include <conio.h>
#include <vector>
#include <stdio.h>
#include <numeric>
#include <time.h>	

#include <Windows.h>
#include <WinBase.h>
#include <omp.h>
#include <mpi.h>
using namespace std;

int main(int argc, char**argv)
{
	setlocale(LC_ALL, "Russian");
	srand(time(0));
	MPI_Init(&argc,&argv);
	MEMORYSTATUS MemStat;
	GlobalMemoryStatus(&MemStat);
	int freemem = MemStat.dwAvailVirtual, sum1=0, sum2=0;
	cout<<"�������� ����������� ������: "<<freemem<<endl;

    int n=freemem/2000;

    cout << "����������� �������� �����: " << n << endl;

    vector<vector<int> > matrix(2, vector<int>(n));
    for (vector<vector<int> >::iterator i = matrix.begin(); i != matrix.end(); ++i)
    {
		
        cout << "������ ����������� �������� ������� �� 1 �� 10" << endl;
        for (vector<int>::iterator j = i->begin(); j != i->end(); ++j)
		{
		*j=rand()%10;
		//system("cls");
		//cout<<*j; �������� ������ ������ ������� ������
		}
		vector<int>::iterator j1 = i->begin(),j2=i->end();
		cout<<"������ (�����������): "<<accumulate(j1, j2, 0)<<endl;
    }
    vector<int> vec1, vec2;

	cout<<"��� �����������������:\n";
	double start_time=MPI_Wtime();
    for (intptr_t i = 0; i < n; ++i)
	   sum1+=matrix[0][i] * matrix[1][i];
	double end_time=MPI_Wtime();
    cout << "��������� ������������ (�����������): " <<  sum1 << endl;
	double search_time = end_time - start_time;
	cout<<"����������� �����: "<<search_time<<endl;

	cout<<"� ��������:\n";

double start_time1;
start_time1=MPI_Wtime();
#pragma omp parallel for //
for (intptr_t i = 0; i < n; ++i)
{
 #pragma omp atomic
	sum2+=matrix[1][i]*matrix[0][i];
}
	double end_time1=MPI_Wtime();
	double search_time1 = end_time1- start_time1;
	cout << "��������� ������������ (�����������): " << sum2 << endl;
	cout<<"����������� �����: "<<search_time1<<endl;
	system("pause");
	MPI_Finalize();
return 0;
}