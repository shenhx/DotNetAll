interface IAnimal{
    name;
  }
  
  class Animal implements IAnimal{
    
    //name; // 等价于在构造器中用修饰字符一样效果
    
    constructor(public name) {
          this.name = name;
          console.log(this.name);
      }
  
       Run(): void{
           console.log(`${this.name} is running!`);
      }
  
      protected Die(): void{
          console.log(`${this.name} is revoked!`);
      }

      // 没意义：练习静态属性
      // static TestStaticProperty: any = "ffdfd";
  }
  
  class Dog extends Animal{
      constructor(name) {
          super(name);
      }
  
      protected KillMouse(mouse: Mouse) {
          if (!mouse) {
              console.log('the mouse is escaped!');
              return;
          }
          console.log('dog have killed mouse!')
          console.log('dog is poisioned!');
          super.Die();
      }

      private _age:number = 0;

      // get set 属性
      public get Age(){
          return this._age;
      }

      public set Age(newAge : number){
          this._age = newAge;
      }

  }
  
  class Mouse extends Animal{
      constructor(name) {
          super(name);
      }
  }
  
  Dog dog = new Dog("Dog");
  Mouse mouse = new Mouse("Mouse");
  dog.Run();
  mouse.Run();
  // dog.Die();//error