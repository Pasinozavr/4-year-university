#include <mpi.h>
#include <stdio.h>
#include <omp.h>
#define TAG_SEND_FWD 99
#define TAG_SEND_BACK 98
#define TAG_REPLY 97
int main(int argc, char* argv[])
{
int k,x;
int myrank, size;
MPI_Status status;
double startTime=0, endTime=0;
MPI_Init(&argc,&argv);
MPI_Comm_size(MPI_COMM_WORLD,&size);
MPI_Comm_rank(MPI_COMM_WORLD,&myrank);
if (myrank == 0) // призначимо один процес головним
{
puts("Running procs forwards"); fflush(stdout); // негайний вивід повідомлення
x=1;
while (x < size)
{
startTime = omp_get_wtime();
MPI_Ssend(&x, 1, MPI_INT, x, TAG_SEND_FWD, MPI_COMM_WORLD);
MPI_Recv (&k, 1, MPI_INT, x, TAG_REPLY, MPI_COMM_WORLD, &status);
printf("Reply from proc %d received %d\n",x,k); fflush(stdout);
x++;
endTime = omp_get_wtime();
}
printf("time1=%d\n",endTime-startTime);
puts("Running procs backwards"); fflush(stdout);
x=size-1;
while (x >0)
{

MPI_Send(&x,1, MPI_INT, x, TAG_SEND_BACK, MPI_COMM_WORLD);
x--;

}
startTime = omp_get_wtime();
}

else // інші процеси - підлеглі
{

MPI_Recv(&k, 1, MPI_INT, 0, TAG_SEND_FWD, MPI_COMM_WORLD, &status);
printf("Proc %d received %d\n",myrank,k); fflush(stdout);
MPI_Ssend(&k,1, MPI_INT, 0, TAG_REPLY, MPI_COMM_WORLD);
MPI_Recv(&k, 1, MPI_INT, 0, TAG_SEND_BACK, MPI_COMM_WORLD, &status);
printf("Proc %d Received %d\n",myrank,k); fflush(stdout);
}
endTime = omp_get_wtime();
printf("time2=%d",endTime-startTime);
MPI_Finalize();
return 0;
}