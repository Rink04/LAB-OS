////Требования к языкам программирования: JAVA, C++, python, assembler
////Разработать программу на нескольких языках, реализующую следующую математическую функцию в цикле
// f(i+1) = f(i) + от(n=i)до(100000000) Σ(b * 2 + c - i)
////и сравнить производительность в зависимости от языка

public class Main {

    public static void main(String[] args) {

        System.out.println(" lab_4, Паршина А Д");
        int numIterrations = 100000000;
        long m;

        for (int j = 0; j < 5; j++) {
            int a = 1, b = 2, c = 3;
            m = System.currentTimeMillis();

            for (int i = 0; i < numIterrations; i++) {
                a += b * 2 + c - i;
            }
            System.out.println("Ответ:");
            System.out.println(a);
            System.out.println("Время");
            System.out.println((double)(System.currentTimeMillis() - m) * 1.000 + " с");
        }
    }
}

// ответ
// -187459711
//32.0 c

