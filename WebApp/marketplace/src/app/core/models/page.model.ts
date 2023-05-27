export class Page<T> {
    items: T[];
    pageIndex: number;
    pageCount: number;
  
    constructor(items: T[], pageIndex: number, pageCount: number) {
      this.items = items;
      this.pageIndex = pageIndex;
      this.pageCount = pageCount;
    }
  
    get nextPageIndex(): number | null {
      return this.pageIndex < this.pageCount - 1 ? this.pageIndex + 1 : null;
    }
  
    get previousPageIndex(): number | null {
      return this.pageIndex > 0 ? this.pageIndex - 1 : null;
    }
  
    getNextPageIndexes(): number[] {
      const nextIndexes: number[] = [];
      for (let i = 1; i <= 3; i++) {
        const nextPageIndex = this.pageIndex + i;
        if (nextPageIndex < this.pageCount) {
          nextIndexes.push(nextPageIndex);
        } else {
          break;
        }
      }
      return nextIndexes;
    }
  
    getPreviousPageIndexes(): number[] {
      const previousIndexes: number[] = [];
      for (let i = 1; i <= 3; i++) {
        const previousPageIndex = this.pageIndex - i;
        if (previousPageIndex >= 0) {
          previousIndexes.unshift(previousPageIndex);
        } else {
          break;
        }
      }
      return previousIndexes;
    }
  }
  