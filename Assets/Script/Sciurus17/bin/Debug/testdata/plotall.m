%�ȑO�̎��s�f�[�^����āA�ϐ���������
close all;
clear variables ;

%�������ʂ̓ǂݍ���(x��)
xdata = dlmread('xdata.txt',',',1,0);
tdata = dlmread('tdata.txt',',',1,0);
time = tdata(:,1);
deg2 = xdata(:,1);
deg5 = xdata(:,2);
deg7 = xdata(:,3);
v2= xdata(:,4);
v5= xdata(:,5);
v7= xdata(:,6);

%�\���͈͂̐ݒ�
xlimmin = 0.0;
%xlimmax = 22.0;


%�O���t��(x��)

figure(1)
hold on;
plot(time,deg2,'LineWidth',1.5)
%legend('x[m]','��Q��')
xlabel('time[s]','FontSize',12,'FontWeight','bold') 
ylabel('ID=2[rad]','FontSize',12,'FontWeight','bold')
xlim([0 18.5]);
set(gca,'FontSize',12);  
%title('�p�x�̕ω�')
hold off;

figure(2)
hold on;
plot(time,deg5,'LineWidth',1.5)
%legend('x[m]','��Q��')
xlabel('time[s]','FontSize',12,'FontWeight','bold') 
ylabel('ID=5[rad]','FontSize',12,'FontWeight','bold')
set(gca,'FontSize',12); 
xlim([0 15]);
%title('�p�x�̕ω�')
hold off;

figure(3)
hold on;
plot(time,deg7,'LineWidth',1.5)
%legend('x[m]','��Q��')
xlabel('time[s]','FontSize',12,'FontWeight','bold') 
ylabel('ID=7[rad]','FontSize',12,'FontWeight','bold')
set(gca,'FontSize',12);  
xlim([0 14.5]);
%title('�p�x�̕ω�')
hold off;

figure(4)
hold on;
plot(time,v2,'LineWidth',1.5)
%legend('x[m]','��Q��')
xlabel('time[s]','FontSize',12,'FontWeight','bold') 
ylabel('ID=2[rad/s]','FontSize',12,'FontWeight','bold')
set(gca,'FontSize',12);  
xlim([0 18.5]);
%title('�p���x�̕ω�')
hold off;

figure(5)
hold on;
plot(time,v5,'LineWidth',1.5)
%legend('x[m]','��Q��')
xlabel('time[s]','FontSize',12,'FontWeight','bold') 
ylabel('ID=5[rad/s]','FontSize',12,'FontWeight','bold')
set(gca,'FontSize',12);  
%title('�p���x�̕ω�')
xlim([0 15]);
hold off;

figure(6)
hold on;
plot(time,v7,'LineWidth',1.5)
%legend('x[m]','��Q��')
xlabel('time[s]','FontSize',12,'FontWeight','bold') 
ylabel('ID=7[rad/s]','FontSize',12,'FontWeight','bold')
set(gca,'FontSize',12);  
xlim([0 14.5]);
%title('�p���x�̕ω�')
hold off;