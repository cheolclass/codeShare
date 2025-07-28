#include <stdio.h>

int main(void)
{

    // comments
    for(int i=0; i<100; i++)      
        for(int j=0; j<100; j++)
            printf("%d x %d = %.0lf \n",
                i, j, i*j);

    return 0;
}